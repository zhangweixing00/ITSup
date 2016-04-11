<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Index.Master" AutoEventWireup="true" CodeBehind="TimeRate.aspx.cs" Inherits="ITSupportPlatform.Pages.TimeRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_header" runat="server">
    <script src="../Scripts/TimeRate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group">
        <label for="weekSelect" class="col-lg-1  control-label">选择周：</label>
        <div class="col-lg-4">
            <select id='weekSelect' class="form-control"></select>
        </div>
    </div>
    <div>
        <div id="chartContainer_EveryWeek" style="margin-left: 10px;"></div>
    </div>
</asp:Content>
