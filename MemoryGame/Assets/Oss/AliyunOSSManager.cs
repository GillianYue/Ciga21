using System;
using System.IO;
using UnityEngine;
using Aliyun.OSS;
using Aliyun.OSS.Common;
namespace MopubOSS
{
    public class AliyunOSSManager
    {
        private string endpoint = "oss-cn-hangzhou.aliyuncs.com";
        private string accessKeyId = "LTAI5tRZgDotyXe4A68DYmdf";
        private string accessKeySecret = "myQGA8MahnJP0rPKv9BOJcfjFtWKHH";
        private string bucketName = "jiuchongshilian-res";

        private string downloadPathPrefix = "https://jiuchongshilian-res.oss-cn-hangzhou.aliyuncs.com";

        private OssClient ossClient;

        static private AliyunOSSManager instance;

        static public AliyunOSSManager GetInstance()
        {
            if (instance == null)
            {
                instance = new AliyunOSSManager();
            }
            return instance;
        }

        public AliyunOSSManager()
        {
            ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
        }

        /**
         * 该方法上传一个视频文件
         * 
         * filePath: 文件路径
         * fileName: 文件名，该文件名只是用于存到oss时用的文件名
         * 
         * 返回该文件的下载链接
         */
        public string UploadVideo(string filePath, string fileName)
        {
            try
            {
                ossClient.PutObject(bucketName, "video/" + fileName, filePath);
            }
            catch (Exception ex)
            {
                Debug.Log("catch: " + ex.Message);
                return "";
            }
            

            return downloadPathPrefix + "/video/" + fileName;
        }

        /**
         * 该方法上传一个视频文件
         * 
         * content: 文件流
         * fileName: 文件名，该文件名只是用于存到oss时用的文件名
         * 
         * 返回该文件的下载链接
         */
        public string UploadVideo(Stream content, string fileName)
        {
            try
            {
                ossClient.PutObject(bucketName, "video/" + fileName, content);
            }
            catch (Exception ex)
            {
                Debug.Log("catch: " + ex.Message);
                return "";
            }
            

            return downloadPathPrefix + "/video/" + fileName;
        }

        /**
         * 该方法上传一个图片文件
         * 
         * filePath: 文件路径
         * fileName: 文件名，该文件名只是用于存到oss时用的文件名
         * 
         * 返回该文件的下载链接
         */
        public string UploadImage(string filePath, string fileName)
        {
            try
            {
                ossClient.PutObject(bucketName, "image/" + fileName, filePath);
            }
            catch (Exception ex)
            {
                Debug.Log("catch: " + ex.Message);
                return "";
            }

            return downloadPathPrefix + "/image/" + fileName;
        }

        /**
         * 该方法上传一个图片文件
         * 
         * content: 文件流
         * fileName: 文件名，该文件名只是用于存到oss时用的文件名
         * 
         * 返回该文件的下载链接
         */
        public string UploadImage(Stream content, string fileName)
        {
            try
            {
                ossClient.PutObject(bucketName, "image/" + fileName, content);
            }
            catch (Exception ex)
            {
                Debug.Log("catch: " + ex.Message);
                return "";
            }

            return downloadPathPrefix + "/image/" + fileName;
        }

    }
}
