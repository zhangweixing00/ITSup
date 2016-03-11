<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statsis.aspx.cs" Inherits="ITSupportPlatform.Pages.Statsis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,Chrome=1" />
    <title>IT</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/common.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>


    <!--[if lte IE 9]>
        <link href="../Content/bootstrap-ie8.css" rel="stylesheet" />
<script src="../Scripts/respond.min.js"></script>
<script src="../Scripts/html5.js"></script>
<![endif]-->


</head>
<body>
    <div class="container-fluid">
        <div class="page-header">
            <h1>IT支持平台 <small>数据统计</small></h1>
        </div>
        <form id="form1" runat="server" class="form-horizontal">


            <div>
                <ul class="nav nav-pills">
                    <li role="presentation" class="active"><a href="#">支持单查询</a></li>

                    <%--                    <li role="presentation"><a href="#">完成率统计</a></li>
                    <li role="presentation"><a href="#">其他</a></li>--%>
                </ul>
                <div class="panel panel-default">

                    <div class="panel-body">

                        <div class="form-group">
                            <label for="tbStartUser" class="col-lg-1  control-label">问题提出人</label>
                            <div class="col-lg-4">
                                <asp:TextBox ID="tbStartUser" runat="server" CssClass="form-control"></asp:TextBox>

                                <%--<input type="text" class="form-control" id="tbStartUser" placeholder="" runat="server" />--%>
                            </div>

                            <label for="tbEndUser" class="col-lg-1  control-label">问题处理人</label>
                            <div class="col-lg-4">
                                <asp:TextBox ID="tbEndUser" runat="server" CssClass="form-control"></asp:TextBox>

                                <%--<input type="text" class="form-control" id="tbEndUser" placeholder="" runat="server" />--%>
                            </div>
                        </div>


                        <div class="form-group">
                            <label for="tbStartTime" class="col-lg-1  control-label">开始时间</label>
                            <div class="col-lg-4">
                                <asp:TextBox ID="tbStartTime" runat="server" CssClass="form-control" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                <%--<input type="text" class="form-control" id="tbStartTime" placeholder="2016-3-3" runat="server"/>--%>
                            </div>

                            <label for="tbEndTime" class="col-lg-1  control-label">结束时间</label>
                            <div class="col-lg-4">
                                <asp:TextBox ID="tbEndTime" runat="server" CssClass="form-control" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                <%--                                <input type="text" class="form-control" id="tbEndTime" placeholder="" runat="server"/>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="tbQuestion" class="col-lg-1  control-label">问题描述</label>
                            <div class="col-lg-4">
                                <asp:TextBox ID="tbQuestion" runat="server" CssClass="form-control"></asp:TextBox>

                                <%--                                <input type="text" class="form-control" id="tbQuestion" placeholder="..." runat="server" />--%>
                            </div>
                            <div class="col-lg-offset-1 col-lg-1">
                                <asp:Button ID="btnQuery" runat="server" Text="查询" class="btn btn-default" OnClick="btnQuery_Click" />
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnExport" runat="server" Text="导出" class="btn btn-default" OnClick="btnExport_Click" />
                            </div>
                        </div>
                        <div style="overflow-x: auto;">
                            <asp:GridView ID="GVList" runat="server" CssClass="List" AutoGenerateColumns="False" AllowPaging="True" DataSourceID="ODSData" PagerStyle-CssClass="anpager">
                                <Columns>
                                    <asp:BoundField DataField="FormID" HeaderText="编号" />
                                    <asp:BoundField DataField="CName" HeaderText="系统名称" />
                                    <asp:BoundField DataField="QName" HeaderText="问题类型" />
                                    <asp:BoundField DataField="CreateByUserName" HeaderText="发起人" />
                                    <asp:BoundField DataField="SumitTime" HeaderText="发起时间" />
                                    <asp:TemplateField HeaderText="所属公司">
                                        <ItemTemplate>
                                            <span title="<%#Eval("CreateDeptName").ToString()%>">
                                                <%# Eval("CreateDeptName").ToString().Length>10?Eval("CreateDeptName").ToString().Substring(0,8)+"...":Eval("CreateDeptName").ToString() %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="问题描述">
                                        <ItemTemplate>
                                            <span title="<%#Eval("ContentTxt").ToString()%>">
                                                <%# Eval("ContentTxt").ToString().Length>10?Eval("ContentTxt").ToString().Substring(0,10)+"...":Eval("ContentTxt").ToString() %>
                                            </span>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="状态">

                                        <ItemTemplate>

                                            <%# Eval("WFStatus").ToString()=="3"?"结束":"进行中" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ApproveByUserName" HeaderText="处理人" />
                                    <asp:TemplateField HeaderText="处理意见">

                                        <ItemTemplate>
                                            <span title="<%#Eval("ApproveOption").ToString()%>">
                                                <%# Eval("ApproveOption").ToString().Length>10?Eval("ApproveOption").ToString().Substring(0,10)+"...":Eval("ApproveOption").ToString() %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FinishedTime" HeaderText="完成时间" />
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ODSData" runat="server" SelectMethod="GetNotifyInfos" TypeName="ITSupportPlatform.Services.ITSupportHelper">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="tbStartUser" Name="startUser" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="tbEndUser" Name="endUser" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="tbStartTime" Name="startTime" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="tbEndTime" Name="endTime" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="tbQuestion" Name="question" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>

                </div>
            </div>

        </form>
    </div>
</body>
</html>
