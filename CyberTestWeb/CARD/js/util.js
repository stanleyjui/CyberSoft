//版本顯示
function ShowMarquee() {
    window.status = "CyberCArd 2.3.010(2015)";
    setTimeout("ShowMarquee();", 50);
}

//判斷欄位是否為空值
function IsEmpty(source, args) {
    if (args.Value == "") {
        args.IsValid = false;
    }
    else {
        args.IsValid = true;
    }
}
//GridView單選
function setRadio(nowRadio, gdv, rtn) {
    //取得table object
    var row_count = 0;
    var myForm, objRadio;
    myForm = document.forms[0];
    for (var i = 0; i < myForm.length; i++) {
        if (myForm[i].type == "radio" && myForm[i].name.indexOf(gdv) > -1 && myForm[i].name.indexOf(rtn) > -1) {
            //計算是第幾個radio
            row_count++;
            objRadio = myForm[i];
            //取消radio時
            if (objRadio != nowRadio && objRadio.name.indexOf(gdv) > -1 && objRadio.name.indexOf(rtn) > -1) {
                if (objRadio.checked) {
                    objRadio.checked = false;
                }
            }
        }
    }
}
function setRadio2(nowRow, gdv, rtn) {
    var row_count = 0;
    var myForm, objRadio;
    myForm = document.forms[0];
    for (var i = 0; i < myForm.length; i++) {
        if (myForm.elements[i].type == "radio" && myForm[i].name.indexOf(gdv) > -1 && myForm[i].name.indexOf(rtn) > -1) {
            //計算是第幾個radio
            row_count++;
            objRadio = myForm[i];
            //取消radio時
            if (row_count != nowRow && objRadio.name.indexOf(gdv) > -1 && objRadio.name.indexOf(rtn) > -1) {
                if (objRadio.checked) {
                    objRadio.checked = false;
                }
            }
            if (row_count == nowRow && objRadio.name.indexOf(gdv) > -1 && objRadio.name.indexOf(rtn) > -1) {
                objRadio.checked = true;
            }
        }
    }
}

function setRadio3(nowRow, gdv, rtn) {
    var row_count = 0;
    var myForm, objRadio;
    myForm = document.forms[0];
    for (var i = 0; i < myForm.length; i++) {
        if (myForm[i].type == "radio") {
            objRadio = myForm[i];
            //計算是第幾個radio
            if (objRadio.name.indexOf(gdv) > -1 && objRadio.name.indexOf(rtn) > -1) {
                row_count++;
            }
            //取消radio時
            if (row_count != nowRow && objRadio.name.indexOf(gdv) > -1 && objRadio.name.indexOf(rtn) > -1) {
                if (objRadio.checked) {
                    objRadio.checked = false;
                }
            }
            if (row_count == nowRow && objRadio.name.indexOf(gdv) > -1 && objRadio.name.indexOf(rtn) > -1) {
                objRadio.checked = true;
            }
        }
    }
}
function setRadio4(nowRow, gdv, rtn) {
    var row_count = 0;
    var myForm, objRadio;
    myForm = document.forms[0];
    //使用jQuery取精準的物件
    myForm = $("[id*='" + gdv + "']");
    //如果取不到，再用document.forms取
    if (myForm == null) {
        return;
    }
    for (var i = 0; i < myForm.length; i++) {
        if (myForm[i].type == "radio") {
            //計算是第幾個radio
            row_count++;
            //objRadio = myForm.elements[i];
            objRadio = myForm[i];
            //取消radio時
            if (row_count != nowRow && objRadio.name.indexOf(gdv) > -1 && objRadio.name.indexOf(rtn) > -1) {
                if (objRadio.checked) {
                    objRadio.checked = false;
                }
            }
            if (row_count == nowRow && objRadio.name.indexOf(gdv) > -1 && objRadio.name.indexOf(rtn) > -1) {
                objRadio.checked = true;
            }
        }
    }
}
//顯示日曆
function getCalendar(pClientID) {
    //取得pClientID值
    var myForm, str1, str2;
    myForm = document.forms[0];
    for (var i = 0; i < myForm.length; i++) {
        if (myForm.elements[i].type == "text") {
            str1 = myForm.elements[i].name;
            str2 = str1.match(pClientID);
            if (str2 != null) {
                pClientID = myForm.elements[i].id;
                break;
            }
        }
    }
    //取得位置
    var curLeft = posX(pClientID);
    var curtop = posY(pClientID);
    //開啟超連結
    var lUrl = 'Calendar.aspx?DialogClientID=' + pClientID;
    //開啟視窗樣式及位置
    var winstyle = 'width=220,height=200,left=' + curLeft + ',top=' + curtop;
    window.open(lUrl, 'frmCalendar', winstyle);
}
//取得網頁物件位置(x)
function posX(objID) {
    var elmt = document.getElementById(objID);
    var x = 0;
    //繞行 offsetParents
    for (var e = elmt; e; e = e.offsetParent) {
        //把 offsetLeft 值加總
        x += e.offsetLeft;
    }
    x += screenLeft - document.documentElement.scrollLeft;
    return x;
}
//取得網頁物件位置(y)
function posY(objID) {
    var elmt = document.getElementById(objID);
    var y = 0;
    //繞行 offsetParents
    for (var e = elmt; e; e = e.offsetParent) {
        //把 offsetTop 值加總
        y += e.offsetTop;
    }
    //繞行至 document.body
    for (e = elmt.parentNode; e && e != document.body; e = e.parentNode) {
        //減去捲軸值
        if (e.scrollTop) y -= e.scrollTop;
    }
    y += screenTop - document.documentElement.scrollTop + elmt.scrollHeight + 5;
    return y;
}
//全選/全不選
function setCheckBox(nowCheckBox, gdv, rtn) {
    var myForm, objCheckBox;
    myForm = document.forms[0];
    for (var i = 0; i < myForm.length; i++) {
        if (myForm.elements[i].type == "checkbox") {
            objCheckBox = myForm.elements[i];
            if (objCheckBox != nowCheckBox && objCheckBox.name.indexOf(gdv) > -1 && objCheckBox.name.indexOf(rtn) > -1) {
                objCheckBox.checked = nowCheckBox.checked;
            }
        }
    }
}
//全選/全不選
function SetCheckBoxListChecked(checkBoxID, optionID, strCOND) {
    var table = document.getElementById(checkBoxID);
    for (i = 0; i < table.rows.length; i++) {
        for (j = 0; j < table.rows[i].cells.length; j++) {

            if (!table.rows[i].cells[j].childNodes[0]) {
                break;
            } else {
                //如果 table 裡有 checkbox 就勾起來(符合條件)
                if (strCOND == "") {
                    table.rows[i].cells[j].childNodes[0].checked = optionID.checked;
                }
                else {
                    if (strCOND == table.rows[i].cells[j].childNodes[1].innerText.substring(0, strCOND.length)) {
                        table.rows[i].cells[j].childNodes[0].checked = optionID.checked;
                    }
                }
            }
        }
    }
}
//全選/全不選
function SetCBLToTextBox(checkBoxID, TextBoxID) {
    var table = document.getElementById(checkBoxID);
    var TextBox = document.getElementById(TextBoxID);
    k = 0;
    tempString = "";
    TextBox.value = "";
    for (i = 0; i < table.rows.length; i++) {
        for (j = 0; j < table.rows[i].cells.length; j++) {

            if (!table.rows[i].cells[j].childNodes[0]) {
                break;
            }
            else {
                //如果 checkbox 勾選，就將選取的位置放到TextBox
                if (table.rows[i].cells[j].childNodes[0].checked) {
                    tempString += ((i * table.rows[0].cells.length) + j).toString() + ",";
                    k++;
                }
            }
        }
    }
    TextBox.value = k.toString() + " item;" + tempString;
}
//TextBox OnKeyPress
function setKeyMaxToNextObj(currTextBox, nextObj, currMaxLength) {
    var myForm, objCurrTextBox, objNextobj;
    myForm = document.forms[0];
    for (var i = 0; i < myForm.length; i++) {
        if (myForm.elements[i].name.substring(myForm.elements[i].name.length - currTextBox.length) == currTextBox) {
            objCurrTextBox = myForm.elements[i];
            continue;
        }
        if (myForm.elements[i].name.substring(myForm.elements[i].name.length - nextObj.length) == nextObj) {
            objNextObj = myForm.elements[i];
            continue;
        }
    } //endfor
    if (objCurrTextBox.value.length >= currMaxLength) {
        objNextObj.focus();
    }
}
//半型字自動轉全型
function unAsc(currTextBox) {
    var asciiTable = "!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
    var big5Table = "%uFF01%u201D%uFF03%uFF04%uFF05%uFF06%u2019%uFF08%uFF09%uFF0A%uFF0B%uFF0C%uFF0D%uFF0E%uFF0F%uFF10%uFF11%uFF12%uFF13%uFF14%uFF15%uFF16%uFF17%uFF18%uFF19%uFF1A%uFF1B%uFF1C%uFF1D%uFF1E%uFF1F%uFF20%uFF21%uFF22%uFF23%uFF24%uFF25%uFF26%uFF27%uFF28%uFF29%uFF2A%uFF2B%uFF2C%uFF2D%uFF2E%uFF2F%uFF30%uFF31%uFF32%uFF33%uFF34%uFF35%uFF36%uFF37%uFF38%uFF39%uFF3A%uFF3B%uFF3C%uFF3D%uFF3E%uFF3F%u2018%uFF41%uFF42%uFF43%uFF44%uFF45%uFF46%uFF47%uFF48%uFF49%uFF4A%uFF4B%uFF4C%uFF4D%uFF4E%uFF4F%uFF50%uFF51%uFF52%uFF53%uFF54%uFF55%uFF56%uFF57%uFF58%uFF59%uFF5A%uFF5B%uFF5C%uFF5D%uFF5E";
    var result = "";
    var objCurrTextBox = document.getElementById(currTextBox);
    for (var k = 0; k < objCurrTextBox.value.length; k++) {
        var val = objCurrTextBox.value.charAt(k);
        var j = asciiTable.indexOf(val) * 6;
        result += (j > -1 ? unescape(big5Table.substring(j, j + 6)) : val);
    }
    objCurrTextBox.value = result;
}
//驗證日期(大於今日)
function checkGEToday(source, clientside_arguments) {
    var checkDate = new Date(document.getElementById(clientside_arguments).Value);
    var todayDate = new Date();
    var todayYYYY = todayDate.getFullYear();
    var todayMM = todayDate.getMonth();
    var todayDD = todayDate.getDate();
    clientside_arguments.IsValid = true;
    todayDate = new Date(todayYYYY, todayMM, todayDD);
    if (checkDate < todayDate) {
        clientside_arguments.IsValid = false;
        return false;
    }
}
//驗證日期(小於今日)
function checkLEToday(source, clientside_arguments) {
    var checkDate = new Date(document.getElementById(clientside_arguments).Value);
    var todayDate = new Date();
    var todayYYYY = todayDate.getFullYear();
    var todayMM = todayDate.getMonth();
    var todayDD = todayDate.getDate();
    clientside_arguments.IsValid = true;
    todayDate = new Date(todayYYYY, todayMM, todayDD);
    if (checkDate > todayDate) {
        clientside_arguments.IsValid = false;
        return false;
    }
}
//驗證比較日期
function checkGEDateField(src, toObj, date1, date2) {
    var checkDate = new Date(document.getElementById(date1).value);
    var checkDate2 = new Date(document.getElementById(date2).value);
    if (checkDate < checkDate2) {
        toObj.IsValid = false;
    }
}
//將數值字串以特定格式濾鏡蓋過,遮蔽部分資訊
function maskField(srcObj, maskObj, toObj) {
    var strSrc = new String(document.getElementById(srcObj).innerText);
    var strMask = new String(document.getElementById(maskObj).value);
    var arrResult = new Array();
    for (i = 0; i < strSrc.length; i++) {
        if (i < strMask.length) {
            var code = parseInt(strMask.charCodeAt(i));
            if ((code >= 48 && code <= 57) || (code >= 65 && code <= 90) || (code >= 97 && code <= 122)) {
                arrResult[i] = strSrc.charAt(i);
            }
            else {
                arrResult[i] = strMask.charAt(i);
            }
        }
        else {
            arrResult[i] = strSrc.charAt(i);
        }
    }
    document.getElementById(toObj).innerText = arrResult.join("");
}
//文字填滿最大長度時自動跳至指定的TextBox
function nextTextbox(objID, next) {
    var dataLen = document.getElementById(objID).value.length;
    var maxLen = document.getElementById(objID).maxLength;
    if (dataLen == maxLen)
        document.getElementById(next).focus();
}
//置換圖片
function EvImageOverChange(name, action, url) {
    switch (action) {
        case 'in':
            name.src = url;
            break;
        case 'out':
            name.src = url;
            break;
    }
}
//產生MEMO視窗
function fus(event) {
    var obj = event.srcElement ? event.srcElement : event.target;
    obj.blur();
    if (obj.value != "") {
        window.open("ShowMemo.aspx", "備註前5筆", "location=0,scrollbars=1,menubar=0,status=0,toolbar=0,titlebar=0,top=300,left=400,width=430px,height=250px");
    }
}

//產生MEMO視窗
function fus2(event) {
    var obj = event.srcElement ? event.srcElement : event.target;
    obj.blur();
    if (obj.value != "") {
        window.open("../../src/ShowMemo.aspx", "備註前5筆", "location=0,scrollbars=1,menubar=0,status=0,toolbar=0,titlebar=0,top=300,left=400,width=430px,height=250px");
    }
}

//Page_ClientValidate is not defined
function PageValidCover() {
    var isPageValid = true;
    if (typeof (Page_Validators) != "undefined") {
        isPageValid = Page_ClientValidate();
    }
    return isPageValid;
}
var isAltF4 = false;
//用來判斷是否按下[X]  
var isXClose = false;
function logout() {
    if ((event.clientX + 21 > document.body.clientWidth && event.clientY < 0) || (isAltF4)) {
        mess = "確定離開?";
        //代表是真的按下[X]  
        if (!confirm(mess)) {
            isAltF4 = false;
            isXClose = false;
            return '按一下「取消」停留在此頁';
        }
        isXClose = true;
    }
}
function getKeyDown() {
    if ((event.altKey && event.keyCode == 115) || (event.ctrlKey && event.keyCode == 87)) {
        isAltF4 = true;
    }
}
//真的關閉時，要觸發logoutUser  
window.onbeforeunload = logout;
window.onunload = logoutUser;
function logoutUser() {
    //多判斷是不是按下[X]關閉，是的話才做登出  
    if (isXClose) {
        try {
            window.open('../../src/LogOut.aspx', '', 'width=1,height=1,left=1,top=1');
            alert('為了確保您的帳號安全,系統已自動幫您登出');
        }
        catch (e) {
            alert('delete user account error message:' + e.message);
        }
    }
}
function blockUI(message) {
    if (confirm(message)) {
        document.getElementById("fullpage").style.display = 'block';
        $('.fullpage').css({ 'width': $(window).width(), 'height': $(document).height() });
        var dialogHeight = $(window).height() / 2 - $('.progress').height() / 2 - 50;
        var dialogWidth = $(window).width() / 2 - $('.progress').width() / 2;
        $('.progress').css({ 'top': dialogHeight, 'left': dialogWidth });
        return true;
    }
    else {
        alert("此動作已經被取消");
        window.event.returnValue = false;
        return false;
    }
}
//頁簽、KEY ITEM 查詢使用
function blockUI2() {
    document.getElementById("fullpage").style.display = 'block';
    $('.fullpage').css({ 'width': $(window).width(), 'height': $(document).height() });
    var dialogHeight = $(window).height() / 2 - $('.progress').height() / 2 - 50;
    var dialogWidth = $(window).width() / 2 - $('.progress').width() / 2;
    document.getElementById("progress").style.display = 'block';
    $('.progress2').css({ 'top': dialogHeight, 'left': dialogWidth });
}
//頁面按鈕使用
function blockUI3() {
    document.getElementById("fullpage").style.display = 'block';
    $('.fullpage').css({ 'width': $(window).width(), 'height': $(document).height() });
    var dialogHeight = $(window).height() / 2 - $('.progress').height() / 2 - 50;
    var dialogWidth = $(window).width() / 2 - $('.progress').width() / 2;
    $('.progress').css({ 'top': dialogHeight, 'left': dialogWidth });
}
//縱向全選
//cbHeader >> GridView Header 的 CheckBox ClientID
//gdvdata >> cbHeader 所在的 GridView
//cell >> GridView Row 裡面的 CheckBox 所在的第幾個cell
//使用GridView Header 的 CheckBox 全選
function checkALL(cbHeader, gdvdata, cell) {
    for (i = 1; i < gdvdata.rows.length; i++) {
        //判斷有無找到控制項，避免有換頁功能時的Footer
        if ($(gdvdata.rows[i].cells[cell]).find(':checkbox')[0] == undefined) {
            break;
        }
        //如果gdvdata裡的checkbox是啟用狀態才打勾
        if (!$(gdvdata.rows[i].cells[cell]).find(':checkbox')[0].disabled) {
            //將gdvdata裡每個CheckBox的checked屬性設定成跟cbHeader一樣
            $(gdvdata.rows[i].cells[cell]).find(':checkbox')[0].checked = cbHeader.checked;
        }
    }
}
//使用GridView Row 裡面的 CheckBox 全選
function ALLcheck(cbHeader, gdvdata, cell) {
    for (i = 1; i < gdvdata.rows.length; i++) {
        //判斷有無找到控制項，避免有換頁功能時的Footer
        if ($(gdvdata.rows[i].cells[cell]).find(':checkbox')[0] == undefined)
            break;
        //判斷gdvdata裡的checkbox是啟用狀態
        if (!$(gdvdata.rows[i].cells[cell]).find(':checkbox')[0].disabled) {
            //判斷啟用狀態的checkbox是否全打勾
            if ($(gdvdata.rows[i].cells[cell]).find(':checkbox')[0].checked) {
                cbHeader.checked = true;
            }
            else {
                cbHeader.checked = false;
                break;
            }
        }
    }
}
//FOR 權限設定使用
//使用GridView Row 裡面的 CheckBox 全選
function ALLcheck2(cbHeader, gdvdata, cell) {
    for (i = 1; i < gdvdata.rows.length; i++) {
        //判斷有無找到控制項，避免有換頁功能時的Footer
        if ($(gdvdata.rows[i].cells[cell]).find(':checkbox')[0] == undefined)
            break;
        //判斷gdvdata裡的checkbox是啟用狀態
        if (!$(gdvdata.rows[i].cells[cell]).find(':checkbox')[0].disabled) {
            //判斷啟用狀態的checkbox是否全打勾
            if ($(gdvdata.rows[i].cells[cell]).find(':checkbox')[0].checked) {
                //2:修改 5:新增 7:複製 6:儲存 12:取消
                if (cell == 2 || cell == 5 || cell == 7) {
                    $(gdvdata.rows[i].cells[6]).find(':checkbox')[0].checked = true;
                    $(gdvdata.rows[i].cells[12]).find(':checkbox')[0].checked = true;
                }
                cbHeader.checked = true;
            }
            else {
                if (!$(gdvdata.rows[i].cells[2]).find(':checkbox')[0].checked &&
                    !$(gdvdata.rows[i].cells[5]).find(':checkbox')[0].checked &&
                    !$(gdvdata.rows[i].cells[7]).find(':checkbox')[0].checked) {
                    $(gdvdata.rows[i].cells[6]).find(':checkbox')[0].checked = false;
                    $(gdvdata.rows[i].cells[12]).find(':checkbox')[0].checked = false;
                }
                cbHeader.checked = false;
                break;
            }
        }
    }
}
//橫向全選
//cbCell >> GridView 橫向全選 CheckBox ClientID
//gdvdata >> cbCell 所在的 GridView
//row >> GridView Cell 裡面的 CheckBox 所在的是第幾個Row
function checkALL_Horizontal(cbCell, gdvdata, row) {
    for (i = 1; i < gdvdata.rows[row + 1].cells.length; i++) {
        if (!$(gdvdata.rows[row + 1].cells[i]).find(':checkbox')[0].disabled)
            $(gdvdata.rows[row + 1].cells[i]).find(':checkbox')[0].checked = cbCell.checked;
    }
}
function ALLcheck_Horizontal(cbCell, gdvdata, row) {
    for (i = 1; i < gdvdata.rows[row + 1].cells.length; i++) {
        if (!$(gdvdata.rows[row + 1].cells[i]).find(':checkbox')[0].disabled) {
            if ($(gdvdata.rows[row + 1].cells[i]).find(':checkbox')[0].checked)
                cbCell.checked = true;
            else {
                cbCell.checked = false;
                break;
            }
        }
    }
}
//全選全選
//flag H為勾選水平全選，V為勾選垂直全選
function setCheck(flag, gdvdata) {
    switch (flag) {
        case 'V':
            for (r = 1; r < gdvdata.rows.length; r++) {
                for (i = 1; i < gdvdata.rows[r].cells.length; i++) {
                    if ($(gdvdata.rows[r].cells[i]).find(':checkbox')[0] == undefined)
                        break;
                    if (!$(gdvdata.rows[r].cells[i]).find(':checkbox')[0].disabled) {
                        if ($(gdvdata.rows[r].cells[i]).find(':checkbox')[0].checked)
                            $(gdvdata.rows[r]).find(':checkbox')[0].checked = true;
                        else {
                            $(gdvdata.rows[r]).find(':checkbox')[0].checked = false;
                            break;
                        }
                    }
                }
            }
            break;
        case 'H':
            //rows[0]為水平表頭
            for (i = 1; i < gdvdata.rows[0].cells.length; i++) {
                for (r = 1; r < gdvdata.rows.length; r++) {
                    if ($(gdvdata.rows[r].cells[i]).find(':checkbox')[0] == undefined)
                        break;
                    if (!$(gdvdata.rows[r].cells[i]).find(':checkbox')[0].disabled) {
                        if ($(gdvdata.rows[r].cells[i]).find(':checkbox')[0].checked)
                            $(gdvdata.rows[0].cells[i]).find(':checkbox')[0].checked = true;
                        else {
                            $(gdvdata.rows[0].cells[i]).find(':checkbox')[0].checked = false;
                            break;
                        }
                    }
                }
            }
            break;
    }
}
//由GridView外的CheckBox全選GridView內的CheckBox
//cbCheck為GridView外的CheckBox
//gdvdata為要全選的GridView
//cell為GridView內的CheckBox所在的位置
function check_gdv_ALL(cbCheck, gdvdata, cell) {
    for (i = 1; i < gdvdata.rows.length; i++) {
        //判斷有無找到控制項，避免有換頁功能時的Footer
        if ($(gdvdata.rows[i].cells[cell]).find(':checkbox')[0] == undefined)
            break;
        //如果gdvdata裡的checkbox是啟用狀態才打勾
        if (!$(gdvdata.rows[i].cells[cell]).find(':checkbox')[0].disabled) {
            //將gdvdata裡每個CheckBox的checked屬性設定成跟cbHeader一樣
            $(gdvdata.rows[i].cells[cell]).find(':checkbox')[0].checked = cbCheck.checked;
        }
    }
    //找出gdvdata的第x個th內的checkbox
    $(gdvdata).find('th:eq(' + cell + ')').find(':checkbox')[0].checked = cbCheck.checked;
}
//Focus GridView選中列的欄位
//field_name欄位名稱要傳字串
//field_val欄位的值要傳字串
function focus_field(field_name, field_val) {
    //欄位名稱轉大寫
    var field = $('[id$="' + field_name.toUpperCase() + '"]');
    if (field.length != 0) {
        if (field.length > 1) {
            for (i = 0; i < field.length; i++) {
                if (field[i].value == field_val) {
                    field[i].focus();
                    break;
                }
            }
        }
        else {
            field.focus();
        }
    }
    else {
        //欄位名稱轉小寫
        field = $('[id$="' + field_name.toLowerCase() + '"]');
        if (field.length > 1) {
            for (j = 0; j < field.length; j++) {
                if (field[j].value == field_val) {
                    field[j].focus();
                    break;
                }
            }
        }
        else {
            field.focus();
        }
    }
}
//驗證控制項 背景變色 
function Verify_Color() {
    var validator = $('div[id="ctl00_PnlContent"]').find('span');
    var flag = true;
    var controltovalidate_old;
    for (i = 0; i < validator.length; i++) {
        if (i > 0) {
            controltovalidate_old = $('#' + validator[i - 1].controltovalidate)[0];
        }
        var controltovalidate = $('#' + validator[i].controltovalidate)[0];
        if (controltovalidate != undefined && controltovalidate_old != undefined) {
            if (controltovalidate_old.id == controltovalidate.id) {
                if (controltovalidate_old.style.backgroundColor == 'lightsteelblue') {
                    continue;
                }
            }
        }
        if (controltovalidate != undefined) {
            if (validator[i].style.display == 'inline') {
                controltovalidate.style.backgroundColor = 'lightSteelblue';
                flag = false;
            }
            else {
                controltovalidate.style.backgroundColor = '';
            }
        }
    }
    return flag;
}

//驗證checkboxlist至少輸入一項
function checkCheckBox(sender, args) {
    var chkControlId = sender.id.replace("Validator_", "");
    chkControlId = chkControlId.replace("_ctv", "_cbl");
    var options = document.getElementById(chkControlId).getElementsByTagName('input');
    var ischecked = false;
    args.IsValid = false;
    for (i = 0; i < options.length; i++) {
        var opt = options[i];
        if (opt.type == "checkbox") {
            if (opt.checked) {
                ischecked = true;
                args.IsValid = true;
            }
        }
    }
    //驗證失敗時換顏色
    if (args.IsValid == false) {
        document.getElementById(chkControlId).style.backgroundColor = 'lightSteelblue';
    }
    else {
        document.getElementById(chkControlId).style.backgroundColor = '';
    }
}
//驗證下一個金額必須大於上一個
function checkAMT(sender, args) {
    //上一個用ToolTip記
    var beforeControlId = 'ctl00_phdContent_' + sender.title;
    beforeControlId = $('#' + beforeControlId);
    var beforeControlId_value = beforeControlId[0].value.replace(/\,/g, '');
    //目前為sender
    var nowControlId = sender.id.replace("Validator_", "").replace("_ctv", "_txt");
    nowControlId = $('#' + nowControlId);
    var nowControlId_value = nowControlId[0].value.replace(/\,/g, '');
    //判斷目前是否大於上一個
    if (parseInt(beforeControlId_value) >= parseInt(nowControlId_value)) {
        args.IsValid = false;
    }
    //控制項變色
    if (!args.IsValid) {
        nowControlId[0].style.backgroundColor = 'lightSteelblue';
    }
    else {
        nowControlId[0].style.backgroundColor = '';
    }
}
//全選/全不選 取出選取的值放到TextBox
function SetCBLToTextBox_Value(checkBoxListID, TextBoxID) {
    var table = $('[id$=' + checkBoxListID + ']')[0];
    var TextBox = $('[id$=' + TextBoxID + ']')[0];
    k = 0;
    tempString = "";
    TextBox.value = "";
    for (i = 0; i < table.rows.length; i++) {
        for (j = 0; j < table.rows[i].cells.length; j++) {
            if (!table.rows[i].cells[j].childNodes[0]) {
                break;
            }
            else {
                //如果 checkbox 勾選，就將選取的值放到TextBox
                if (table.rows[i].cells[j].childNodes[0].checked) {
                    var item_innerText = table.rows[i].cells[j].innerText;
                    tempString += item_innerText + ",";
                    k++;
                }
            }
        }
    }
    if (k > 0) {
        TextBox.value = k.toString() + " item;" + tempString;
    }
    else {
        TextBox.value = "";
    }
}
//部分全選/全不選 CheckBoxList內的CheckBox(使用UserControl或無法取得正確ID時使用)
function SetCheckBoxListChecked_USC_PART(checkBoxListID, checkBoxListID_checked, optionID) {
    var table = $('[id$=' + checkBoxListID + ']')[0];
    var table_checked = $('[id$=' + checkBoxListID_checked + ']')[0];
    for (r = 0; r < table.rows.length; r++) {
        for (c = 0; c < table.rows[r].cells.length; c++) {

            if (!table.rows[r].cells[c].childNodes[0]) {
                break;
            } else {
                //如果 table 裡有 checkbox 就勾起來(符合條件)
                if (table.rows[r].cells[c].childNodes[0] != undefined) {
                    //有存在於checkBoxListID_checked的選項才勾
                    for (r_checked = 0; r_checked < table_checked.rows.length; r_checked++) {
                        for (c_checked = 0; c_checked < table_checked.rows[r_checked].cells.length; c_checked++) {
                            if (table.rows[r].cells[c].innerText == table_checked.rows[r_checked].cells[c_checked].innerText) {
                                table.rows[r].cells[c].childNodes[0].checked = optionID.checked;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}

//同步選取兩個CheckListBox中的CheckBox(使用UserControl或無法取得正確ID時使用)
function setCheckBox_Sync_USC(nowCheckBox, visibleCheckBox) {
    var visibleCheckBox = $('[id$=' + visibleCheckBox + ']')[0];
    var nowCheckBox = $('[id$=' + nowCheckBox + ']')[0];
    //ROW
    for (r = 0; r < nowCheckBox.rows.length; r++) {
        //CELL
        for (c = 0; c < nowCheckBox.rows[0].cells.length; c++) {
            if (nowCheckBox.rows[r].cells[c].childNodes[0] != undefined) {
                visibleCheckBox.rows[r].cells[c].childNodes[0].checked = nowCheckBox.rows[r].cells[c].childNodes[0].checked;
            }
        }
    }
}

//全選/全不選 選取CheckBoxList內所有CheckBox(使用UserControl或無法取得正確ID時使用)
function SetCheckBoxListChecked_USC(checkBoxListID, optionID) {
    var table = $('[id$=' + checkBoxListID + ']')[0];
    for (r = 0; r < table.rows.length; r++) {
        for (c = 0; c < table.rows[r].cells.length; c++) {

            if (!table.rows[r].cells[c].childNodes[0]) {
                break;
            } else {
                //如果 table 裡有 checkbox 就勾起來(符合條件)
                if (table.rows[r].cells[c].childNodes[0] != undefined) {
                    table.rows[r].cells[c].childNodes[0].checked = optionID.checked;
                }
            }
        }
    }
}
//同步選取兩個CheckListBox中的CheckBox
function setCheckBox_Sync(nowCheckBox, visibleCheckBox) {
    var visibleCheckBox = $('#' + visibleCheckBox)[0];
    var nowCheckBox = $('#' + nowCheckBox)[0];
    //ROW
    for (r = 0; r < nowCheckBox.rows.length; r++) {
        //CELL
        for (c = 0; c < nowCheckBox.rows[0].cells.length; c++) {
            if (nowCheckBox.rows[r].cells[c].childNodes[0] != undefined) {
                visibleCheckBox.rows[r].cells[c].childNodes[0].checked = nowCheckBox.rows[r].cells[c].childNodes[0].checked;
            }
        }
    }
}
//usercontrol類AJAX的ModalPopup功能 (show出DIV及遮罩，DIV的z-index必須>fullpage的z-index)
function show_ModalPopup_DIV(divID, Bool) {
    var div = $('#' + divID);
    var fullpage = $('#fullpage')[0];
    if (div.length > 0) {
        if (Bool) {
            fullpage.style.display = 'block';
            $('.fullpage').css({ 'width': $(window).width(), 'height': $(document).height() });
        }
        else {
            fullpage.style.display = 'none';
        }
        //將div水平及垂直置中顯示
        $(div[0]).css({ 'left': $(window).width() / 2 - $(div[0]).width() / 2, 'top': $(window).height() / 2 - $(div[0]).height() / 2 });
        div.toggle();
    }
}
//功能: 取消鍵盤特殊鍵功能
function click() {
    if (event.keyCode == 44)   //取消Print screen
    {
        event.returnValue = false;
    }
    if (event.ctrlKey) //使用者按下Ctrl鍵
    {
        switch (event.keyCode) {
            case 65: event.keyCode = 0; event.returnValue = false; break;  //取消Ctrl+A 全選
            case 67: event.keyCode = 0; event.returnValue = false; break;  //取消Ctrl+C 複製
            case 80: event.keyCode = 0; event.returnValue = false; break;  //取消Ctrl+P 列印
            case 83: event.keyCode = 0; event.returnValue = false; break;  //取消Ctrl+S Save Page
            case 85: event.keyCode = 0; event.returnValue = false; break;  //取消Ctrl+U Source Code
        }
    }
    //滑鼠右鍵
    if (event.button == 2) {
        alert('為協助保護本行個人資料安全，CyberCArd頁面已自動鎖定複製！');
        return false;
    }
    //
    if (event.keyCode == 93) {
        alert('為協助保護本行個人資料安全，CyberCArd頁面已自動鎖定複製！');
        return false;
    }
}
document.oncontextmenu = function () {
    event.returnValue = false;
}
document.onkeydown = click;
document.onmousedown = click;

function SelectedIndexChanged(ctlId) {
    var control = document.getElementById(ctlId + 'cblSELECT_ITEMS');
    var chks = control.getElementsByTagName('input');
    var lbs = control.getElementsByTagName('label');
    var strSelText = '';
    for (var i = 0; i < chks.length; i++) {
        var chk = chks[i];
        if (chk.checked) {
            strSelText += lbs[i].innerHTML + ';';
        }
    }
    if (strSelText.length > 0)
        strSelText = strSelText.substring(0, strSelText.length - 1);
    var txtSELECT_ITEMS = document.getElementById(ctlId + "txtSELECT_ITEMS");
    txtSELECT_ITEMS.innerHTML = strSelText;
    txtSELECT_ITEMS.innerText = strSelText;
    txtSELECT_ITEMS.title = strSelText;
}

function OpenListBox(ctlId) {
    var pnl = document.getElementById(ctlId + "Panel1");
    var lstBox = document.getElementById(ctlId + "cblSELECT_ITEMS");
    var PanelBOX = document.getElementById(ctlId + "PanelBOX");
    if (PanelBOX.style.visibility == "visible")
    { CloseListBox(ctlId); }
    else {
        PanelBOX.style.visibility = "visible";
        if (lstBox != null) {
            lstBox.style.width = pnl.clientWidth - 3 + 'px';
        }
    }
}

function CloseListBox(ctlId) {
    var PanelBOX = document.getElementById(ctlId + "PanelBOX");
    var tabl = document.getElementById(ctlId + "Table1");
    var panel = document.getElementById(ctlId + "Panel1");
    PanelBOX.style.visibility = "hidden";
    PanelBOX.style.heght = "0px;";
    panel.style.height = tabl.style.height;
}

function changeTab(tabIndex) {
    var tabBehavior = $("div[id$='TabContainer999']")[0].control;
    tabBehavior.set_activeTabIndex(tabIndex);
}

function changeColor() {
    var oElements = document.getElementsByTagName("input");
    for (i = 0; i < oElements.length; i++) {
        if (oElements[i].type == 'checkbox') { var x = oElements[i]; x.onclick = function x() { return false; } }
        if (oElements[i].type == 'radio' && oElements[i].checked == false) { oElements[i].disabled = true; }
        if (oElements[i].type == 'text') { oElements[i].readOnly = true; }
    }
}

function addMarker(trID, divID) {
    $.ajax(
            {
                url: '../../service/getSpot.ashx',
                type: 'post',
                async: false,
                data: {},
                dataType: 'json',
                success: function (data) {
                    var first = true;
                    var map;
                    /*資訊視窗放在迴圈外，content由標記點的html決定*/
                    var infowindow = new google.maps.InfoWindow();
                    var BeforeMerName;
                    var Png = 0;
                    if (data.length <= 0) {
                        return;
                    }
                    for (var index in data) {
                        if (first == true) {//第一次執行迴圈
                            /*以哪個緯經度中心來產生地圖*/
                            var latlng = new google.maps.LatLng(data[index].LATITUDE, data[index].LONGITUDE);
                            var myOptions = {
                                zoom: 8,
                                center: latlng,
                                mapTypeId: google.maps.MapTypeId.ROADMAP
                            };
                            /*產生地圖*/
                            var div = $('#' + divID);
                            map = new google.maps.Map(div[0], myOptions);
                            first = false;
                        } //End if (first == true) 
                        //如果跟上一個特店名稱相同特店名稱加序號
                        if (BeforeMerName == data[index].NAME) {
                            data[index].LONGITUDE = data[index].LONGITUDE + (index * 0.00001);
                            data[index].NAME = data[index].NAME + index;
                        }
                        //記錄此次特店名稱，不記錄index
                        BeforeMerName = data[index].NAME.replace(index, '');
                        //圖片使用index
                        Png++;
                        //建立緯經度座標
                        var myLatlng = new google.maps.LatLng(data[index].LATITUDE, data[index].LONGITUDE);
                        //加一個Marker到map中
                        var marker = new google.maps.Marker({
                            position: myLatlng,
                            map: map,
                            title: data[index].NAME,
                            html: '特店名稱：' + data[index].NAME + '</br>地址：' + data[index].ADDRESS +
                                  '</br>消費金額：' + data[index].AMT + '</br>消費日期：' + data[index].EFF_DTE,
                            icon: '../../images/map/' + Png + '.png'
                        });
                        //為每個標記點註冊click事件
                        google.maps.event.addListener(marker, 'click', function () {
                            /*this就是指marker*/
                            infowindow.setContent(this.html);
                            infowindow.open(map, this);
                        }); //End google.maps.event.addListener
                    } //End for (var index in data) 
                    var tr = $('#' + trID);
                    if (tr.css('display') == 'none') {
                        tr.toggle();
                    }
                }, //End success:
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.statusText);
                    alert(thrownError);
                } //End error:
            });       //End $.ajax
} //End addMarker()   

//ToolTip特別處理
function Mouse_ToolTip() {
    var validator = $('#ctl00_PnlContent').find('[title]');
    var ddlValue = "999";
    for (i = 0; i < validator.length; i++) {
        if ($(validator[i])[0].title != '' && $(validator[i])[0].title != 'undefined') {

            //將enabled屬性轉為readonly
            var disabled = $(validator[i]).attr('disabled');
            if (disabled == 'disabled') {
                if ($(validator[i])[0].id != null) {
                    if ($(validator[i])[0].id.indexOf('lbl') > -1) {
                        $(validator[i]).removeAttr('disabled');
                        $(validator[i]).attr('readOnly', 'readonly');
                        $(validator[i]).css('border-width', '0px');
                    }
                }
                if ($(validator[i])[0].name != null) {
                    if ($(validator[i])[0].name.indexOf('txt') > -1) {
                        $(validator[i]).removeAttr('disabled');
                        $(validator[i]).attr('readOnly', 'readonly');
                        $(validator[i]).css('border-width', '0px');
                    }
                    if (disabled == 'disabled' && $(validator[i])[0].name.indexOf('ddl') > -1 &&
                        $(validator[i])[0].CascadingDropDownParentControlID == null) {
                        $(validator[i]).removeAttr('disabled');
                        $(validator[i]).find('option:not(:selected)').remove();
                    }
                }
            }
            if ($(validator[i])[0].title == 'memo') {
                $(validator[i]).removeAttr('title');
            }
            if ($(validator[i])[0].title != '' && $(validator[i])[0].title != 'undefined') {

                $(validator[i]).hover(function () {
                    ddlValue = "999";
                    var $this = $(this);
                    if ($this.hasClass('active')) return;

                    // Hover over code
                    var title = $(this)[0].title;

                    if (title != "") {
                        $(this).data('tipText', title).removeAttr('title');
                        $(this)[0].title = "";
                        $('<p class="DemoTooltip"></p>')
                        .text(title)
                        .appendTo('body')
                        .fadeIn('slow');
                    }
                }, function () {

                    if ($(this).hasClass('active')) return;

                    // Hover out code
                    $(this).attr('title', $(this).data('tipText'));
                    $(this)[0].title = $(this).data('tipText');
                    $('.DemoTooltip').remove();
                }).mousemove(function (e) {

                    if ($(this).hasClass('active')) return;

                    var mousex = e.pageX; //Get X coordinates
                    var mousey = e.pageY; //Get Y coordinates
                    $('.DemoTooltip').css({ top: mousey + 5, left: mousex })
                }).click(function () {	// 當滑鼠點擊時
                    var disabled = $(this).find('option:not(:selected)').attr('disabled');
                    if (disabled != 'disabled') {
                        if ($(this)[0].selectedIndex != ddlValue) {
                            if ($(this)[0].id.indexOf('ddl') > -1) {
                                $this = $(this).addClass('active'); //暫停tooltip (for 下拉式選單)
                            }
                        }
                    }
                    $('.DemoTooltip').remove();
                }).change(function () { //當下拉式選單的值變更
                    if ($(this)[0].id.indexOf('ddl') > -1) {
                        ddlValue = $(this)[0].selectedIndex;
                        $this = $(this).removeClass('active'); //恢復tooltip (for 下拉式選單)
                        $(this).attr('title', $(this).data('tipText'));
                        $('.DemoTooltip').remove();
                    }
                });
            }
        }
    }
}