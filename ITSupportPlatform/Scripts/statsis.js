$(function () {

    InitSelect();

    LoadDatas_EveryWeek($("#weekSelect").val());
    LoadDatas_Current();
    CreateTigger();
});
function CreateTigger() {
    setInterval("Refresh()", 30 * 1000); //1000为1秒钟60 * 6
}
function Refresh() {
    $("#chartContainer_real").html("正在刷新数据，请稍后......");
    setTimeout("LoadDatas_Current()", 1000);
}


var LoadDatas_Current = function () {
    var title = "实时";
    var cid = "chartContainer_real";
    LoadDatas(GetCurentWeekMonday(), CurentTime(), title, cid, GetCurentWeekMonday() + "~当前" );
}


var startTime = '2016-2-29'
var InitSelect = function () {
    var now = new Date();
    var count = now.DateDiff("w", startTime) * (-1);
    //alert(count);
    var opTxt = "";
    for (var index = 1; index < count; index++) {
        opTxt += "<option value='" + index + "'>第" + index + "周</option>"
    }
    opTxt += "<option value='" + index + "' selected='selected'>第" + index + "周</option>"
    $("#weekSelect").html(opTxt);
    $("#weekSelect").change(function () { LoadDatas_EveryWeek($("#weekSelect").val()); });
}

var LoadDatas_EveryWeek = function (index) {
    //alert(index);
    var start = StringToDate(startTime).DateAdd('d', (index - 1) * 7);
    //alert(start);
    var end = StringToDate(startTime).DateAdd('d', (index) * 7).DateAdd('s', -1);
    var title = "历史单周";
    var cid = "chartContainer_EveryWeek";
    LoadDatas(start.ShowTime(), end.ShowTime(), title, cid, start.ShowTime() + "~" + end.ShowTime());
}


function ShowTable(title, cid, obj, subTitle, startTime, endTime) {
    var tab = "<div class='tabletitle'>" + title + "</div>";
    tab += "<div class='tableSubtitle'>[" + subTitle + "]</div>";
    tab += "<table class='List'><tr><th>姓名</th><th>未完成支持率</th><th>需完成数</th><th>未完成数</th></tr>";
    $.each(obj, function (i, n) {
        tab += "<tr><td>" + n.EmployeeName + "</td><td>" + n.result + "%</td><td>" + n.Total;
        if (n.UnFinished == "0") {
            tab += "</td><td>" + n.UnFinished + "</td></tr>"

        }
        else {
           // tab += "</td><td><a href='Statsic_DetailMore.aspx?startTime=" + startTime + "&endTime=" + endTime + "&user=" + n.UName + "'>" + n.UnFinished + "</a></td></tr>"
            tab += "</td><td><a href='javascript:LoadDatasMore(&#39;" + startTime + "&#39;,&#39;" + endTime + "&#39;,&#39;" + n.UName + "&#39;)'>" + n.UnFinished + "</a></td></tr>"
        }
    });
    tab += "</table>";
    $("#" + cid).html(tab);
}
var LoadDatas = function (startTime, endTime, title, cid, subTitle) {
    var datas = new Object();
    datas.sT = startTime;
    datas.eT = endTime;
    //datas.Parms = new Array();
    //var datas = new Object();
    //datas.Parms = new Array();
    //datas.Parms[0] = startTime;
    //datas.Parms[1] = endTime;
    //datas.RequestName = "ITsupport_4DX_Statsic";
    datas = window.JSON.stringify(datas);
    $.ajax({
        async: false,
        type: "Post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: '/Services/AjaxService.svc/FourDx',
        data: datas,
        success: function (data) {

            var responseData = data.d;
            //alert(responseData.Length);
            ShowTable(title, cid, responseData, subTitle, startTime, endTime);

        },
        error: function (data) {
            alert("errorText:" + data.message);
        }
    });
}

function ShowTableMore(cid, obj, startTime, endTime) {
    var tab = "";
   // tab += "<div class='tableSubtitle'>[" + subTitle + "]</div>";
    tab += "<table class='List'><tr><th>编号</th><th>标题名称</th><th>发起时间</th><th>完成时间</th><th>用时（小时）</th></tr>";
    $.each(obj, function (i, n) {
        tab += "<tr><td>" + n.FormID + "</td><td><a href='http://zybpm.founder.com/Workflow/ViewPage/V_OA_ITSupport.aspx?ID=" + n.InstanceID + "' target='_blank'>" + n.FormTitle + "</a></td><td>" + n.SumitTime + "</td><td>" + n.FinishedTime + "</td><td>" + n.FinishHour + "</td></tr>";
    });
    tab += "</table>";
    
    $("#" + cid).html(tab);
}
    
var LoadDatasMore = function (startTime, endTime, user) {
    var datas = new Object();
    datas.sT = startTime;
    datas.user = user;
    datas.eT = endTime;
    //datas.Parms = new Array();
    //var datas = new Object();
    //datas.Parms = new Array();
    //datas.Parms[0] = startTime;
    //datas.Parms[1] = endTime;
    //datas.RequestName = "ITsupport_4DX_Statsic";
    datas = window.JSON.stringify(datas);
    $.ajax({
        async: false,
        type: "Post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        url: '/Services/AjaxService.svc/FourDxDetail',
        data: datas,
        success: function (data) {

            var responseData = data.d;
            //alert(responseData.Length);
            ShowTableMore("modalbody_div", responseData, startTime, endTime);
            $('#myModal').modal('show');

        },
        error: function (data) {
            alert("errorText:" + data.message);
        }
    });
}