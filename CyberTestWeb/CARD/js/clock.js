var now, years, months, days, weeks, hours, minutes, seconds, timevalue, timevalue2;
function showtime() {
    var monthname = new Array("01", "02", "03", "04", "05", "06", "07", "08", "09",
 "10", "11", "12");
    var weeksname = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    now = new Date();
    years = now.getFullYear();
    months = now.getMonth();
    days = now.getDate();
    days = ((days < 10) ? "0" : "") + days;
    weeks = now.getDay();
    hours = now.getHours();
    minutes = now.getMinutes();
    seconds = now.getSeconds();
    timevalue = " ";
    timevalue += hours + ":";
    timevalue += ((minutes < 10) ? "0" : "") + minutes + ":";
    timevalue += ((seconds < 10) ? "0" : "") + seconds + "  ";
    document.getElementById("clock").innerHTML = timevalue;
    timevalue2 = years + "." + monthname[months] + "." + days + " ";
    document.getElementById("twcalendat").innerHTML = timevalue2;
    setTimeout("showtime()", 1000);
}
var ServerDate;
function showServertime() {
    ServerDate = parseInt($("#hfServerTime").val());
    SetDate(ServerDate);
}
function SetDate() {
    ServerDate += 1000;
    var t = new Date(ServerDate);
    yyyy = t.getFullYear();
    mm = ((t.getMonth() + 1) < 10) ? "0" + (t.getMonth() + 1) : (t.getMonth() + 1);
    dd = (t.getDate() < 10) ? "0" + t.getDate() : t.getDate();
    h = (t.getHours() < 10) ? "0" + t.getHours() : t.getHours();
    m = (t.getMinutes() < 10) ? "0" + t.getMinutes() : t.getMinutes();
    s = (t.getSeconds() < 10) ? "0" + t.getSeconds() : t.getSeconds();
    t = yyyy + "/" + mm + "/" + dd + " " + h + ":" + m + ":" + s;
    $("#twcalendat").text(t);
    setTimeout("SetDate();", 1000);
}
//倒數計時420秒(預設，但會因系統設定的選項改變)
var secs = 420;
function countdown() {
    if (document.getElementById("countflag").value == "1") {
        //secs = 420; 重新倒數
        secs = document.getElementById("hdfAUTO_LOGOFF_DT").value;
        document.getElementById("countflag").value = "0";
        //自動調整PnlContent的大小
        autoSize();
    }
    else
        if (document.getElementById("countflag").value == "2") {
            //secs = 120; 登出提醒
            secs = document.getElementById("hdfAUTO_LOGOFF_DT").value - document.getElementById("hdfPRE_LOGOFF_DT").value;
            document.getElementById("countflag").value = "0";
            $('.fullpage').css({ 'width': $(window).width(), 'height': $(document).height() });
        }
    var mm = Math.floor("0" + (secs % 3600) / 60);
    var ss = ("0" + secs % 60).slice(-2);
    //顯示倒數資訊
    document.getElementById("countdown1").innerHTML = mm + ":" + ss;
    document.getElementById("countdown2").innerHTML = mm + ":" + ss;
    secs--;
    setTimeout("countdown()", 1000);
}
function tabFocus() {
    if (document.getElementById("ctl00_tabFocus") == null) return;
    var s = document.getElementById("ctl00_tabFocus").value;
    if (s != "") {
        if (document.getElementById(s) != null) {
            window.setTimeout('document.getElementById("' + s + '").focus();', 200);
        }
    }

}
function Focus(textbox) {
    if (document.getElementById(textbox) == null) return;
    var objtextbox = document.getElementById(textbox);
    objtextbox.focus();
    objtextbox.value = objtextbox.value;
}
function setDDLProduct() {
    var product = $('#hdfPRODUCT').val();
    if (product != "") {
        $('#ddlwhereproduct option[value=' + product + ']').attr('selected', true);
        $('#hdfPRODUCT').val("");
    }
}
function autoSize() {
    var height = $('.PnlContent').height();
    if (height < 600) {
        $('.PnlContent').height(600);
    }
    //自動調整下載頁及網路傳輸中的位置
    var dialogHeight = $(window).height() / 2 - $('.progress').height() / 2 - 50;
    var dialogWidth = $(window).width() / 2 - $('.progress').width() / 2;
    $('.progress').css({ 'top': dialogHeight, 'left': dialogWidth });
}