using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using HydrantWiki.Library.Constants;
using HydrantWiki.Library.Managers;
using HydrantWiki.Library.Objects;
using HydrantWiki.Mobile.Api.Extensions;
using HydrantWiki.Mobile.Api.Helpers;
using HydrantWiki.Mobile.Api.Objects;
using HydrantWiki.Mobile.Api.ResponseObjects;
using Nancy;
using Newtonsoft.Json;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Geospatial.Helpers;
using TreeGecko.Library.Geospatial.Objects;
using TreeGecko.Library.Net.Objects;
using Tag = HydrantWiki.Library.Objects.Tag;
using User = HydrantWiki.Library.Objects.User;

namespace HydrantWiki.Mobile.Api.Modules
{
    public class ApiModule: NancyModule
    {
        public ApiModule()
        {
            Get["/api/authorize"] = _parameters => Response.AsError(HttpStatusCode.MethodNotAllowed, null);

            Post["/api/authorize"] = _parameters =>
            {
                BaseResponse br = Authorize(_parameters);
                return Response.AsSuccess(br);
            };

            Get["/api/user/isavailable/{username}"] = _parameters =>
            {
                BaseResponse br = IsAvailable(_parameters);
                return Response.AsSuccess(br);
            };

            Get["/api/tags/count"] = _parameters =>
            {
                BaseResponse br = HangleGetTagCount(_parameters);
                return Response.AsSuccess(br);
            };

            Get["/api/tags/mine/count"] = _parameters =>
            {
                BaseResponse br = HandleGetMyTagCount(_parameters);
                return Response.AsSuccess(br);
            };

            Post["/api/tag"] = _parameters =>
            {
                BaseResponse br = HandleTagPost(_parameters);
                return Response.AsSuccess(br);
            };

            Post["/api/image/{fileName}"] = _parameters =>
            {
                BaseResponse br = HandleImagePost(_parameters);
                return Response.AsSuccess(br);
            };

            Post["/api/image"] = _parameters =>
            {
                BaseResponse br = HandleImagePost(_parameters);
                return Response.AsSuccess(br);
            };

            Get["/api/hydrants/box/{east:decimal}/{west:decimal}/{north:decimal}/{south:decimal}"] = _parameters =>
            {
                BaseResponse br = HangleGetHydrantsByGeobox(_parameters);
                return Response.AsSuccess(br);
            };

            Get["/api/hydrants/box/{east:decimal}/{west:decimal}/{north:decimal}/{south:decimal}/{quantity:int}"] = _parameters =>
            {
                BaseResponse br = HangleGetHydrantsByGeobox(_parameters);
                return Response.AsSuccess(br);
            };

            Get["/api/hydrants/circle/{latitude:decimal}/{longitude:decimal}/{distance:decimal}"] = _parameters =>
            {
                BaseResponse br = HangleGetHydrantsByCenterDistance(_parameters);
                return Response.AsSuccess(br);
            };

            Get["/api/tags/mine/{page:int}/{pagesize:int}"] = _parameters =>
            {
                BaseResponse br = HandleGetMyTags(_parameters);
                return Response.AsSuccess(br);
            };
        }

        private BaseResponse HandleGetMyTags(DynamicDictionary _parameters)
        {
            User user;
            BaseResponse response;

            if (AuthHelper.IsAuthorized(Request, out user))
            {
                int page = _parameters["page"];
                int pageSize = _parameters["pagesize"];
                
                HydrantWikiManager hwm = new HydrantWikiManager();

                List<Tag> tags = hwm.GetTagsForUser(user.Guid, page, pageSize);
                List<Objects.Tag> outputTags = new List<Objects.Tag>();

                foreach (Tag tag in tags)
                {
                    Objects.Tag outputTag = new Objects.Tag(tag);
                    outputTags.Add(outputTag);
                }

                int count = hwm.GetTagCount(user.Guid);
                int pages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count)/Convert.ToDouble(pageSize)));

                response = new TagsResponse(outputTags, page, pageSize, pages, count);
            }
            else
            {
                response = new BaseResponse
                {
                    Success = false,
                    Error = "Unauthorized",
                    Message = "Reauthenticate"
                };
            }

            return response;
        }

        private BaseResponse HangleGetTagCount(DynamicDictionary _parameters)
        {
            User user;
            BaseResponse response;

            if (AuthHelper.IsAuthorized(Request, out user))
            {
                HydrantWikiManager hwm = new HydrantWikiManager();
                int count = hwm.GetTagCount();

                response = new TagCountResponse(true, count);
            }
            else
            {
                response = new BaseResponse
                {
                    Success = false,
                    Error = "Unauthorized",
                    Message = "Reauthenticate"
                };
            }

            return response;
        }

        private BaseResponse HandleGetMyTagCount(DynamicDictionary _parameters)
        {
            User user;
            BaseResponse response;
            
            if (AuthHelper.IsAuthorized(Request, out user))
            {
                HydrantWikiManager hwm = new HydrantWikiManager();
                int count = hwm.GetTagCount(user.Guid);

                response = new TagCountResponse(true, count);
            }
            else
            {
                response = new BaseResponse
                {
                    Success = false,
                    Error = "Unauthorized",
                    Message = "Reauthenticate"
                };
            }

            return response;
        }

        private BaseResponse Authorize(
            DynamicDictionary _parameters)
        {
            User user;
            BaseResponse response = AuthHelper.Authorize(Request, out user);
            return response;
        }

        private BaseResponse IsAvailable(DynamicDictionary _parameters)
        {
            HydrantWikiManager hwm = new HydrantWikiManager();
            IsAvailableResponse response = new IsAvailableResponse {Available = false, Success = true};

            string username = _parameters["username"];
            User user = hwm.GetUser(UserSources.HydrantWiki, username);

            if (user == null)
            {
                response.Available = true;
            }

            return response;
        }

        private BaseResponse HandleImagePost(DynamicDictionary _parameters)
        {
            var response = new BaseResponse { Success = false };
            User user;

            if (AuthHelper.IsAuthorized(Request, out user))
            {
                HydrantWikiManager hwManager = new HydrantWikiManager();

                byte[] fileData = null;
                string fileName = null;

                if (Request.Files.Any())
                {
                    HttpFile file = Request.Files.First();

                    long length = file.Value.Length;
                    fileData = new byte[(int) length];
                    file.Value.Read(fileData, 0, (int) length);
                    fileName = file.Name;
                }

                if (fileName != null)
                {
                    string tempGuid = Path.GetFileNameWithoutExtension(fileName);
                    Guid imageGuid;

                    if (Guid.TryParse(tempGuid, out imageGuid))
                    {
                        try
                        {
                            hwManager.PersistOriginal(imageGuid, ".jpg", "image/jpg", fileData);
                            hwManager.LogVerbose(user.Guid, "Tag Image Saved");

                            Image original = ImageHelper.GetImage(fileData);

                            fileData = ImageHelper.GetThumbnailBytesOfMaxSize(original, 800);
                            hwManager.PersistWebImage(imageGuid, ".jpg", "image/jpg", fileData);

                            fileData = ImageHelper.GetThumbnailBytesOfMaxSize(original, 100);
                            hwManager.PersistThumbnailImage(imageGuid, ".jpg", "image/jpg", fileData);

                            response.Success = true;
                            return response;
                        }
                        catch (Exception ex)
                        {
                            hwManager.LogException(user.Guid, ex);
                        }
                    }
                }
            }
            else
            {
                response.Error = "Unauthorized";
                response.Message = "Reauthenticate";
            }

            return response;
        }

        private BaseResponse HandleTagPost(DynamicDictionary _parameters)
        {
            var response = new TagPostResponse { Success = false };
            User user;

            if (AuthHelper.IsAuthorized(Request, out user))
            {
                HydrantWikiManager hwManager = new HydrantWikiManager();

                string json = Request.Body.ReadAsString();

                Objects.Tag tag = JsonConvert.DeserializeObject<Objects.Tag>(json);

                if (tag != null)
                {
                    if (tag.Position != null)
                    {
                        if (tag.Position != null)
                        {
                            var dbTag = new HydrantWiki.Library.Objects.Tag
                            {
                                Active = true,
                                DeviceDateTime = tag.Position.DeviceDateTime,
                                LastModifiedDateTime = DateTime.UtcNow,
                                UserGuid = user.Guid,
                                VersionTimeStamp = DateTime.UtcNow.ToString("u"),
                                Position = new GeoPoint(tag.Position.Longitude, tag.Position.Latitude),
                                ImageGuid = tag.ImageGuid,
                                Status = TagStatus.Pending
                            };

                            try
                            {
                                hwManager.Persist(dbTag);
                                hwManager.LogVerbose(user.Guid, "Tag Saved");

                                response.Success = true;
                                return response;
                            }
                            catch (Exception ex)
                            {
                                hwManager.LogException(user.Guid, ex);
                            }
                        }
                        else
                        {
                            //No position
                            hwManager.LogWarning(user.Guid, "No position");

                            response.Message = "No position";
                            return response;
                        }
                    }
                }
            }
            else
            {
                response.Error = "Unauthorized";
                response.Message = "Reauthenticate";
            }

            return response;
        }

        public BaseResponse HangleGetHydrantsByCenterDistance(DynamicDictionary _parameters)
        {
            double latitude = Convert.ToDouble((string)_parameters["latitude"]);
            double longitude = Convert.ToDouble((string)_parameters["longitude"]);
            double distance = Convert.ToDouble((string)_parameters["distance"]);

            HydrantWikiManager hwm = new HydrantWikiManager();
            GeoPoint center = new GeoPoint(longitude, latitude);

            List<Hydrant> hydrants = hwm.GetHydrants(center, distance);

            List<HydrantHeader> headers = ProcessHydrants(hydrants, center);

            HydrantQueryResponse response = new HydrantQueryResponse {Success = true, Hydrants = headers};

            return response;
        }

        public BaseResponse HangleGetHydrantsByGeobox(DynamicDictionary _parameters)
        {
            HydrantWikiManager hwm = new HydrantWikiManager();

            double east = Convert.ToDouble((string)_parameters["east"]);
            double west = Convert.ToDouble((string)_parameters["west"]);
            double north = Convert.ToDouble((string)_parameters["north"]);
            double south = Convert.ToDouble((string)_parameters["south"]);

            int quantity = 250;
            if (_parameters.ContainsKey("quantity"))
            {
                quantity = Convert.ToInt32((string) _parameters["quantity"]);
            }
            if (quantity > 500)
            {
                quantity = 500;
            }

            GeoBox geobox = new GeoBox(east, west, north, south);

            List<Hydrant> hydrants = hwm.GetHydrants(geobox, quantity);

            List<HydrantHeader> headers = ProcessHydrants(hydrants);

            HydrantQueryResponse response = new HydrantQueryResponse { Success = true, Hydrants = headers };

            return response;
        }

        private List<HydrantHeader> ProcessHydrants(IEnumerable<Hydrant> _hydrants, GeoPoint _center = null)
        {
            HydrantWikiManager hwm = new HydrantWikiManager();
            Dictionary<Guid, string> users = new Dictionary<Guid, string>();

            var output = new List<HydrantHeader>();
            foreach (var hydrant in _hydrants)
            {
                string username;
                Guid userGuid = hydrant.OriginalTagUserGuid;

                if (users.ContainsKey(userGuid))
                {
                    username = users[userGuid];
                }
                else
                {
                    TGUser user = hwm.GetUser(userGuid);
                    users.Add(user.Guid, user.Username);
                    username = user.Username;
                }

                var outputHydrant = new HydrantHeader
                {
                    HydrantGuid = hydrant.Guid,
                    Position = new GeoLocation(hydrant.Position.Y, hydrant.Position.X, 0),
                    ThumbnailUrl = hydrant.ThumbnailUrl,
                    ImageUrl = hydrant.ImageUrl,
                    Username = username
                };

                if (_center == null)
                {
                    outputHydrant.DistanceInFeet = null;
                }
                else
                {
                    outputHydrant.DistanceInFeet = PositionHelper.GetDistance(_center, hydrant.Position).ToFeet();
                }

                output.Add(outputHydrant);
            }

            return output;
        }
    }
}