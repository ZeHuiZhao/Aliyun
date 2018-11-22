<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="T4.Index" %>

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
    <script type="text/javascript">

        $(function () {

            $("#btnAdd").click(function () {

                var form = $(this).parents("form")[0];
                var formdata = new FormData(form);
                $.ajax({
                    url: 'Index.aspx?cmd=add',
                    type: 'POST',
                    cache: false,
                    data: formdata,
                    processData: false,
                    contentType: false
                }).done(function (res) {
                    $("form input[type=reset]").trigger("click");
                    $('#myModal').modal('hide');
                    window.location.reload();
                }).fail(function (res) {

                    alert("fail");
                });
            });

            $("table .btn-download").click(function () {

                var key = $(this).data("key");
                $("body").append('<iframe  height="0" src="Index.aspx?cmd=downloadfile&key=' + key + '"></iframe>');
            });

            $("table .btn-delete").click(function () {

                if (!window.confirm("确定要删除吗？"))
                    return;
                var id = $(this).data("id");

                $.post("Index.aspx", { cmd: "delete", id: id }, function () {
                    window.location.reload();

                })
            });
        });
    </script>
</head>
<body>
    <div class="container">
        <p class="page-header h3">阿里云对象存储Demo</p>
        <div class="btn-group" role="group" aria-label="...">
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">新增</button>
        </div>
        <table class="table table-bordered table-condensed table-hover">
            <tr>
                <th>姓名</th>
                <th>类别</th>
                <th>创建日期</th>
                <th>Id</th>
                <th>附件</th>
                <td>操作</td>
            </tr>
            <asp:Repeater ID="rpt" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.GetValue<T4.UserSalary>().Name %></td>
                        <td><%# Container.GetValue<T4.UserSalary>().Cat.ToString() %></td>
                        <td><%# Container.GetValue<T4.UserSalary>().CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        <td><%# Container.GetValue<T4.UserSalary>().Id %></td>
                        <td>
                            <button type="button" class="btn btn-link btn-download" data-key="<%# Container.GetValue<T4.UserSalary>().SSOTag %>"><%# Container.GetValue<T4.UserSalary>().SSOTag %></button></td>
                        <td>
                            <button type="button" class="btn btn-link btn-delete" data-id="<%# Container.GetValue<T4.UserSalary>().Id %>">删除</button>
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
                                        <option value="<%# (int)Container.GetValue<T4.UserSalary.Category>() %>"><%#Container.GetValue<T4.UserSalary.Category>().ToString() %></option>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputFile">附件</label>
                            <input type="file" name="AttachmentFile" id="exampleInputFile" />
                            <p class="help-block">Example block-level help text here.</p>
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
