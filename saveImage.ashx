<%@ WebHandler Language="C#" Class="saveImage" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;

public class saveImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            string filePath = "";
            filePath = context.Server.MapPath(".");
            string fileName = RandomString(10);
            string myImage = context.Request.Form["image"];
            if (myImage.Length > 0)
            {
                File.WriteAllBytes(filePath + "/upload/" + fileName + ".jpg", Convert.FromBase64String(myImage));
                context.Response.ContentType = "text/plain";
                context.Response.Write("File was saved - " + fileName + ".jpg");
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("File was not saved");
            }
        }
        catch (Exception ex)
        {

            context.Response.ContentType = "text/plain";
            context.Response.Write(ex.Message);
        }
    }

    private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
    private string RandomString(int size)
    {
        StringBuilder builder = new StringBuilder();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }

        return builder.ToString();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}