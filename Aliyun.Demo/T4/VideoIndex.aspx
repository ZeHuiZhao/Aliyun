<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoIndex.aspx.cs" Inherits="T4.VideoIndex" %>

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
    <script src="aliyun-upload-sdk-1.4.0/lib/es6-promise.min.js"></script>
    <script src="aliyun-upload-sdk-1.4.0/lib/aliyun-oss-sdk-5.2.0.min.js"></script>
    <script src="aliyun-upload-sdk-1.4.0/aliyun-upload-sdk-1.4.0.min.js"></script>
    <script type="text/javascript">

        $(function () {

            $("#btnAdd").click(function () {
                var form = $(this).parents("form")[0];
                var formdata = new FormData(form);
                formdata.append("cmd", "add");
                $.ajax({//先拿到videoid和上传凭证等
                    url: '',
                    type: 'POST',
                    cache: false,
                    data: formdata,
                    processData: false,
                    contentType: false
                }).done(function (response) {//浏览器直接访问阿里云上传
                    var uploader = new AliyunUpload.Vod({
                        //分片大小默认1M，不能小于100K
                        partSize: 101*1024,
                        //并行上传分片个数，默认5
                        parallel: 5,
                        //网络原因失败时，重新上传次数，默认为3
                        retryCount: 3,
                        //网络原因失败时，重新上传间隔时间，默认为2秒
                        retryDuration: 2,
                        // 开始上传
                        'onUploadstarted': function (uploadInfo) {
                            //设定videoId authen等信息
                            uploader.setUploadAuthAndAddress(uploadInfo,response.UploadAuth,response.UploadAddress,response.VideoId);
                            log("onUploadStarted:" + uploadInfo.file.name + ", endpoint:" + uploadInfo.endpoint + ", bucket:" + uploadInfo.bucket + ", object:" + uploadInfo.object);
                            //上传方式1, 需要根据uploadInfo.videoId是否有值，调用点播的不同接口获取uploadauth和uploadAddress，如果videoId有值，调用刷新视频上传凭证接口，否则调用创建视频上传凭证接口
                            // uploader.setUploadAuthAndAddress(uploadInfo, uploadAuth, uploadAddress,videoId);
                            //上传方式2
                            // uploader.setSTSToken(uploadInfo, accessKeyId, accessKeySecret,secretToken);
                        },
                        // 文件上传成功
                        'onUploadSucceed': function (uploadInfo) {

                            //$("form input[type=reset]").trigger("click");
                            //$('#myModal').modal('hide');
                            //window.location.reload();

                            log("onUploadSucceed: " + uploadInfo.file.name + ", endpoint:" + uploadInfo.endpoint + ", bucket:" + uploadInfo.bucket + ", object:" + uploadInfo.object);
                        },
                        // 文件上传失败
                        'onUploadFailed': function (uploadInfo, code, message) {
                            log("onUploadFailed: file:" + uploadInfo.file.name + ",code:" + code + ", message:" + message);
                        },
                        // 文件上传进度，单位：字节
                        'onUploadProgress': function (uploadInfo, totalSize, loadedPercent) {
                            //这里处理进度
                            $("form .progress-bar").css("width", 100*loadedPercent+"%");
                            $("form .progress-bar span").text(100 * loadedPercent + "%");
                            log("onUploadProgress:file:" + uploadInfo.file.name + ", fileSize:" + totalSize + ", percent:" + Math.ceil(loadedPercent * 100) + "%");
                        },
                        // 上传凭证超时
                        'onUploadTokenExpired': function (uploadInfo) {
                            console.log("onUploadTokenExpired");
                            //上传方式1  实现时，根据uploadInfo.videoId调用刷新视频上传凭证接口重新获取UploadAuth
                            // uploader.resumeUploadWithAuth(uploadAuth);
                            // 上传方式2 实现时，从新获取STS临时账号用于恢复上传
                            // uploader.resumeUploadWithSTSToken(accessKeyId, accessKeySecret, secretToken, expireTime);
                        },
                        //全部文件上传结束
                        'onUploadEnd': function (uploadInfo) {
                            log("onUploadEnd: uploaded all the files");
                        }
                    });

                  
                    //uploader.addFile(event.target.files[i], null, null, null, userData);
                    uploader.addFile(document.getElementById("exampleInputFile").files[0], null, null, null, '');
                    uploader.startUpload();


                  
                }).fail(function (res) {

                    alert("fail");
                });
            });


            $("table .btn-delete").click(function () {

                if (!window.confirm("确定要删除吗？"))
                    return;
                var id = $(this).data("id");

                $.post("", { cmd: "delete", id: id }, function () {
                    window.location.reload();

                })
            });
        });

        function aliyunhandler() {
            
        }
    </script>
</head>
<body>
    <div class="container">
        <p class="page-header h3">阿里云视频点播Demo</p>
        <div class="btn-group" role="group" aria-label="...">
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">新增</button>
        </div>
        <table class="table table-bordered table-condensed table-hover">
            <tr>
                <th>姓名</th>
                <th>类别</th>
                <th>创建日期</th>
                <th>Id</th>
                <th>视频</th>
                <td>操作</td>
            </tr>
            <asp:Repeater ID="rpt" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.GetValue<T4.UserVideo>().Name %></td>
                        <td><%# Container.GetValue<T4.UserVideo>().Cat.ToString() %></td>
                        <td><%# Container.GetValue<T4.UserVideo>().CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        <td><%# Container.GetValue<T4.UserVideo>().Id %></td>
                        <td>
                            <a class="btn btn-link" target="_blank" href="PlayerVideo.aspx?VideoId=<%# Container.GetValue<T4.UserVideo>().SSOTag %>"><%# Container.GetValue<T4.UserVideo>().SSOTag %></a></td>
                        <td>
                            <button type="button" class="btn btn-link btn-delete" data-id="<%# Container.GetValue<T4.UserVideo>().Id %>">删除</button>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Modal title</h4>
                </div>
                <div class="modal-body">
                    <form enctype="multipart/form-data" method="post" action="Index.aspx?cmd=add">
                        <div class="form-group">
                            <label for="exampleInputEmail1">姓名</label>
                            <input type="text" name="Name" class="form-control" placeholder="" />
                        </div>
                        <div class="form-group">
                            <label for="exampleInputPassword1">类别</label>
                            <select name="Cat" class="form-control">
                                <asp:Repeater ID="rptCat" runat="server">
                                    <ItemTemplate>
                                        <option value="<%# (int)Container.GetValue<T4.UserVideo.Category>() %>"><%#Container.GetValue<T4.UserVideo.Category>().ToString() %></option>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputFile">视频</label>
                            <input type="file" name="AttachmentFile" multiple="multiple" id="exampleInputFile" />
                            <p class="help-block">Example block-level help text here.</p>
                        </div>
                        <div class="progress">
                            <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0">
                                <span></span>
                            </div>
                        </div>
                        <input type="reset" value="重置" class=" hidden" />
                        <button type="button" class="btn btn-default" id="btnAdd">确定</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>--%>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
