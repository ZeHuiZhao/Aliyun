<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayerVideo.aspx.cs" Inherits="T4.PlayerVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <style>
        /*.modal-dialog{
            position:fixed;
            right:0;
            top:0;
            bottom:0;
            margin:0;
        }*/
    </style>
   <%-- <script src="aliyun-upload-sdk-1.4.0/lib/es6-promise.min.js"></script>
    <script src="aliyun-upload-sdk-1.4.0/lib/aliyun-oss-sdk-5.2.0.min.js"></script>
    <script src="aliyun-upload-sdk-1.4.0/aliyun-upload-sdk-1.4.0.min.js"></script>--%>
    <link rel="stylesheet" href="//g.alicdn.com/de/prismplayer/2.7.2/skins/default/aliplayer-min.css" />
     <script charset="utf-8" type="text/javascript" src="//g.alicdn.com/de/prismplayer/2.7.2/aliplayer-min.js"></script>
    <script type="text/javascript">

        var vid="<%=this.VideoId%>";
        $(function () {

            $.post("", { cmd: "PlayAuth", VideoId: vid }, function (data) {
                var player = new Aliplayer({
                    "id": "player-con",
                    "vid": data.VideoId,
                    "playauth": data.PlayAuth,
                    "qualitySort": "asc",
                    "format": "mp4",
                    "mediaType": "video",
                    "width": "100%",
                    "height": "500px",
                    "autoplay": true,
                    "isLive": false,
                    "rePlay": false,
                    "playsinline": true,
                    "preload": true,
                    "controlBarVisibility": "hover",
                    "useH5Prism": true
                }, function (player) {
                    
                });
            })
        });

       
    </script>
</head>
<body>
    <div class="container">
        <p class="page-header h3">阿里云视频点播Demo-播放器</p>
        
        <div class="prism-player" id="player-con"></div>

    </div>
    
</body>
</html>
