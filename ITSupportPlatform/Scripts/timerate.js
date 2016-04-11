$(function () {

    InitSelect();

    LoadDatas_EveryWeek($("#weekSelect").val());
});


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
    tab += "<table class='List'><tr><th>姓名</th><th>总耗时</th><th>处理个数</th><th>时效</th></tr>";
    $.each(obj, function (i, n) {
        tab += "<tr><td>" + n.UName + "</td><td>" + n.Total + "</td><td>" + n.Num + "</td><td>" + n.Rate + "</td>";
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
        url: '/Services/AjaxService.svc/TimeRate',
        data: datas,
        success: function (data) {

            var responseData = data.d;
            //alert(responseData.Length);
            ShowTable(title, cid, responseData, subTitle, startTime, endTime);

        },
        error: function (data) {
            $("#chartContainer_EveryWeek").html(window.JSON.stringify(data));
           // alert("errorText:" +  window.JSON.stringify(data));
        }
    });
}
