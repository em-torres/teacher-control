using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
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

        public static IActionResult Ok(this Controller controller, Func<JObject> method)
        {
            if (controller.ModelState.IsValid)
            {
                try
                {
                    JObject json = method();
                    if (json.HasValues)
                    {
                        return controller.Json(json);
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

        public static IActionResult Ok(this Controller controller, Func<JArray> method)
        {
            if (controller.ModelState.IsValid)
            {
                try
                {
                    JArray json = method();
                    if (json.HasValues)
                    {
                        return controller.Json(json);
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
