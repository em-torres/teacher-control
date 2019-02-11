using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;

namespace TeacherControl.API.Extensors
{
    public static class HttpResponseExtensors
    {
        public static IActionResult Created(this Controller controller, Func<JObject> method)
        {
            if (controller.ModelState.IsValid)
            {
                try
                {
                    JObject json = method();
                    if (json.HasValues)
                    {
                        return controller.Created(controller.Url.Action(), json);
                    }

                    return controller.BadRequest("Something went wrong");//TODO: Improve the err message
                }
                catch (SqlException ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
                    };//TODO: get a more valuable sqlserver error
                    return json;
                }
                catch (Exception ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
                    };//TODO: get a more valuable exception error
                    return json;
                }
            }

            return controller.BadRequest(controller.ModelState);
        }

        public static IActionResult Ok<T>(this Controller controller, Func<IEnumerable<T>> method) where T : class
        {
            if (controller.ModelState.IsValid)
            {
                try
                {
                    IEnumerable<T> data = method();

                    string pageSize = controller.Request.Query.Where(i => i.Key.ToLower().Equals("page_size")).FirstOrDefault().Value.ToString();
                    string offset = controller.Request.Query.Where(i => i.Key.ToLower().Equals("offset")).FirstOrDefault().Value.ToString();
                    int size = pageSize.Length > 0 ? int.Parse(pageSize) : 50;
                    int skip = offset.Length > 0 ? int.Parse(offset) : 0;

                    IEnumerable<T> filtedData = data.Skip(size * skip).Take(size > 0 ? size : 50);
                    if (filtedData.Count() > 0)
                    {
                        return controller.Ok(JArray.FromObject(filtedData));
                    }

                    return controller.NotFound("No Values Found");
                }
                catch (SqlException ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
                    };//TODO: get a more valuable sqlserver error
                    return json;
                }
                catch (Exception ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
                    };//TODO: get a more valuable sqlserver error
                    return json;
                }
            }

            return controller.BadRequest(controller.ModelState);
        }

        public static IActionResult Ok(this Controller controller, Func<JObject> method)
        {
            if (controller.ModelState.IsValid)
            {
                try
                {
                    JObject json = method();
                    if (json.HasValues)
                    {
                        return controller.Ok(json);
                    }

                    return controller.BadRequest(controller.ModelState);
                }
                catch (SqlException ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
                    };//TODO: get a more valuable sqlserver error
                    return json;
                }
                catch (Exception ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
                    };//TODO: get a more valuable sqlserver error
                    return json;
                }
            }

            return controller.BadRequest(controller.ModelState);
        }

        public static IActionResult NoContent(this Controller controller, Func<bool> method)
        {
            if (controller.ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = method();
                    if (isSuccess)
                    {
                        return controller.NoContent();
                    }

                    return controller.BadRequest(controller.ModelState);
                }
                catch (SqlException ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
                    };//TODO: get a more valuable sqlserver error
                    return json;
                }
                catch (Exception ex)
                {
                    JsonResult json = new JsonResult(ex)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
                    };//TODO: get a more valuable sqlserver error
                    return json;
                }
            }

            return controller.BadRequest(controller.ModelState);
        }

    }
}
