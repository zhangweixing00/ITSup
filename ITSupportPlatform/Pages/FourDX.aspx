<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Index.Master" AutoEventWireup="true" CodeBehind="FourDX.aspx.cs" Inherits="ITSupportPlatform.Pages.FourDX" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_header" runat="server">
    <script src="../Scripts/statsis.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="form-group">
            <label for="weekSelect" class="col-lg-1  control-label">选择周：</label>
            <div class="col-lg-4">
                <select id='weekSelect' class="form-control"></select>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-5">

            <div id="chartContainer_EveryWeek"></div>
        </div>
        <div class="col-lg-2"></div>
        <div class="col-lg-5">
            <div id="chartContainer_real">
                请稍后.....
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" >
        <div class="modal-dialog" role="document" style="width:800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">超时详情</h4>
                </div>
                <div class="modal-body" id="modalbody_div" >
                   
                </div>
            </div>
        </div>
    </div>
</asp:Content>
