using System;
using System.Linq;
using HydrantWiki.Library.Constants;
using HydrantWiki.Library.Managers;
using HydrantWiki.Library.Objects;
using HydrantWiki.Mobile.Api.ResponseObjects;
using Nancy;
using TreeGecko.Library.Net.Objects;

namespace HydrantWiki.Mobile.Api.Helpers
{
    public static class AuthHelper
    {
        public static bool IsAuthorized(Request _request, out User _user)
        {
            HydrantWikiManager hwManager = new HydrantWikiManager();

            string username = _request.Headers["Username"].First();
            string authToken = _request.Headers["AuthorizationToken"].First();

            User user = hwManager.GetUser(UserSources.HydrantWiki, username);
            if (user != null)
            {
                TGUserAuthorization userAuth = hwManager.GetUserAuthorization(user.Guid, authToken);

                if (userAuth != null
                    && !userAuth.IsExpired())
                {
                    _user = user;

                    return true;
                }
            }

            _user = null;
            return false;
        }

        public static BaseResponse Authorize(string _username, string _password, out User _user)
        {
            AuthorizationResponse authResponse = new AuthorizationResponse();
            authResponse.Success = false;

            HydrantWikiManager hwManager = new HydrantWikiManager();
            _user = hwManager.GetUser(UserSources.HydrantWiki, _username);

            if (_user != null)
            {
                if (_user.IsVerified)
                {
                    if (_user.Active)
                    {
                        if (hwManager.ValidateUser(_user, _password))
                        {
                            TGUserAuthorization authorization =
                                TGUserAuthorization.GetNew(_user.Guid, "unknown");
                            hwManager.Persist(authorization);

                            var user = new HydrantWiki.Mobile.Api.Objects.User();
                            user.AuthorizationToken = authorization.AuthorizationToken;
                            user.DisplayName = _user.DisplayName;
                            user.Username = _user.Username;

                            authResponse.Success = true;
                            authResponse.User = user;
                            authResponse.Message = "";

                            return authResponse;
                        }

                        //Bad password or username
                        hwManager.LogWarning(Guid.Empty, "User not found");
                        authResponse.Message = "Bad user or password";

                        return authResponse;
                    }

                    //user not active
                    //Todo - Log Something
                    hwManager.LogWarning(_user.Guid, "User Not Active");
                    authResponse.Message = "User not active";
                    return authResponse;
                }

                //User not verified
                //Todo - Log Something
                hwManager.LogWarning(_user.Guid, "User not verified");
                authResponse.Message = "User not verified";
                return authResponse;
            }

            //User not found
            hwManager.LogWarning(Guid.Empty, "User not found");
            authResponse.Message = "User not found";
            return authResponse;
        }

        public static BaseResponse Authorize(Request _request, out User _user)
        {
            string username = _request.Headers["Username"].First();
            string password = _request.Headers["Password"].First();

            return Authorize(username, password, out _user);
        }
    }
}