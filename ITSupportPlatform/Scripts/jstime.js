Date.prototype.DateAdd = function (strInterval, Number) {
    var dtTmp = this;
    switch (strInterval) {
        case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
}

/* 计算两日期相差的日期年月日等 */
//Date.prototype.dateDiff = function (interval, objDate2) {
//    alert(d.getTime());
//    var d = this, i = {}, t = d.getTime(), t2 = objDate2.getTime();
//    
//    i['y'] = objDate2.getFullYear() - d.getFullYear();
//    i['q'] = i['y'] * 4 + Math.floor(objDate2.getMonth() / 4) - Math.floor(d.getMonth() / 4);
//    i['m'] = i['y'] * 12 + objDate2.getMonth() - d.getMonth();
//    i['ms'] = objDate2.getTime() - d.getTime();
//    i['w'] = Math.floor((t2 + 345600000) / (604800000)) - Math.floor((t + 345600000) / (604800000));
//    i['d'] = Math.floor(t2 / 86400000) - Math.floor(t / 86400000);
//    i['h'] = Math.floor(t2 / 3600000) - Math.floor(t / 3600000);
//    i['n'] = Math.floor(t2 / 60000) - Math.floor(t / 60000);
//    i['s'] = Math.floor(t2 / 1000) - Math.floor(t / 1000);
//    return i[interval];
//}
function StringToDate(DateStr) {

    var converted = Date.parse(DateStr);
    var myDate = new Date(converted);
    if (isNaN(myDate)) {
        //var delimCahar = DateStr.indexOf('/')!=-1?'/':'-';   
        var arys = DateStr.split('-');
        myDate = new Date(arys[0], --arys[1], arys[2]);
    }
    return myDate;
}
Date.prototype.DateDiff = function (strInterval, dtEnd) {
    var dtStart = this;
    if (typeof dtEnd == 'string')//如果是字符串转换为日期型   
    {
        dtEnd = StringToDate(dtEnd);
    }
   // alert(dtStart);
    switch (strInterval) {
        case 's': return parseInt((dtEnd - dtStart) / 1000);
        case 'n': return parseInt((dtEnd - dtStart) / 60000);
        case 'h': return parseInt((dtEnd - dtStart) / 3600000);
        case 'd': return parseInt((dtEnd - dtStart) / 86400000);
        case 'w': return parseInt((dtEnd - dtStart) / (86400000 * 7));
        case 'm': return (dtEnd.getMonth() + 1) + ((dtEnd.getFullYear() - dtStart.getFullYear()) * 12) - (dtStart.getMonth() + 1);
        case 'y': return dtEnd.getFullYear() - dtStart.getFullYear();
    }
}   
//function DateDiff(interval, date1, date2) {
//    var TimeCom1 = new TimeCom(date1);
//    var TimeCom2 = new TimeCom(date2);
//    var result;
//    switch (String(interval).toLowerCase()) {
//        case "y":
//        case "year":
//            result = TimeCom1.year - TimeCom2.year;
//            break;
//        case "m":
//        case "month":
//            result = (TimeCom1.year - TimeCom2.year) * 12 + (TimeCom1.month - TimeCom2.month);
//            break;
//        case "d":
//        case "day":
//            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day)) / (1000 * 60 * 60 * 24));
//            break;
//        case "h":
//        case "hour":
//            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day, TimeCom1.hour) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day, TimeCom2.hour)) / (1000 * 60 * 60));
//            break;
//        case "min":
//        case "minute":
//            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day, TimeCom1.hour, TimeCom1.minute) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day, TimeCom2.hour, TimeCom2.minute)) / (1000 * 60));
//            break;
//        case "s":
//        case "second":
//            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day, TimeCom1.hour, TimeCom1.minute, TimeCom1.second) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day, TimeCom2.hour, TimeCom2.minute, TimeCom2.second)) / 1000);
//            break;
//        case "ms":
//        case "msecond":
//            result = Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day, TimeCom1.hour, TimeCom1.minute, TimeCom1.second, TimeCom1.msecond) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day, TimeCom2.hour, TimeCom2.minute, TimeCom2.second, TimeCom1.msecond);
//            break;
//        case "w":
//        case "week":
//            
//            result = Math.round((Date.UTC(TimeCom1.year, TimeCom1.month - 1, TimeCom1.day) - Date.UTC(TimeCom2.year, TimeCom2.month - 1, TimeCom2.day)) / (1000 * 60 * 60 * 24)) % 7;
//           alert(result); break;
//        default:
//            result = "invalid";
//    }
//    return (result);
//}
function TimeCom( dateValue )
    {
  var newCom;
  if (dateValue=="")
  {
   newCom = new Date();
  }else{
   newCom = new Date(dateValue);
  }
        this.year = newCom.getYear();
        this.month = newCom.getMonth()+1;
        this.day = newCom.getDate();
        this.hour = newCom.getHours();
        this.minute = newCom.getMinutes();
        this.second = newCom.getSeconds();
        this.msecond = newCom.getMilliseconds();
        this.week = newCom.getDay();
    }
function GetCurentWeekMonday() {
    var now = new Date();
    var wday = (now.getDay() + 7) % 8;            //日
    var mon = now.DateAdd('d', -1 * wday);

    var year = mon.getFullYear();       //年
    var month = mon.getMonth() + 1;     //月
    var day = mon.getDate();            //日

    var clock = year + "-";

    if (month < 10)
        clock += "0";

    clock += month + "-";

    if (day < 10)
        clock += "0";

    clock += day;

    return (clock);
}
function GetLastWeekMonday() {
    var now = new Date();
    var wday = (now.getDay() + 7) % 8 + 7;            //日
    var mon = now.DateAdd('d', -1 * wday);

    var year = mon.getFullYear();       //年
    var month = mon.getMonth() + 1;     //月
    var day = mon.getDate();            //日

    var clock = year + "-";

    if (month < 10)
        clock += "0";

    clock += month + "-";

    if (day < 10)
        clock += "0";

    clock += day;

    return (clock);
}

function CurentTime() {
    var now = new Date();

    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var hh = now.getHours();            //时
    var mm = now.getMinutes();          //分

    var clock = year + "-";

    if (month < 10)
        clock += "0";

    clock += month + "-";

    if (day < 10)
        clock += "0";

    clock += day + " ";

    if (hh < 10)
        clock += "0";

    clock += hh + ":";
    if (mm < 10) clock += '0';
    clock += mm;
    return (clock);
}
Date.prototype.ShowTime = function () {

    var now = this;
    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var hh = now.getHours();            //时
    var mm = now.getMinutes();          //分
    var ss = now.getSeconds();          //m

    var clock = year + "-";

    if (month < 10)
        clock += "0";

    clock += month + "-";

    if (day < 10)
        clock += "0";

    clock += day + " ";

    if (hh < 10)
        clock += "0";

    clock += hh + ":";
    if (mm < 10) clock += '0';
    clock += mm+ ":";
    if (ss < 10) clock += '0';
    clock += ss;
    return (clock);
} 