//check欄位是否輸入
//filed:欄位,para1:名稱
//一律於submit時作check
function IsRequired(field,fieldname){
if (field.value.length==0){
alert(fieldname + '不可空白');
field.focus();
field.select();
return false;
}
}


//最小值
function IsMinRange(field,minPara,fieldname) {
if (field.value.length>0)
{
var mynum = parseInt(field.value);   
if (isNaN(field.value)&&field.value!="0") {
alert(fieldname + "欄位格式不正確！");
field.focus();
//field.value="";
return false;
} else if (mynum >= parseInt(minPara)) {  
return true;
} else {
alert(fieldname +"不可小於"+ minPara + "！"); 
field.focus();
//field.value="";
return false;
}
}
}

//最大值
function IsMaxRange(field,maxPara,fieldname) {
if (field.value.length>0)
{
var mynum = parseInt(field.value);   
if (isNaN(field.value)&&field.value!="0") {
alert(fieldname + "欄位格式不正確！");
field.focus();
//field.value="";
return false;
} else if (mynum <= parseInt(maxPara)) {  
return true;
} else {
alert(fieldname +"不可大於"+ maxPara + "！");
field.focus();
//field.value=""
return false;
}
}
}

//字串是否有'字元
function IsLegal(field)
{
if (field.value.indexOf("'")!=-1)
{

alert("不可含有 ' 特殊字元！");
field.focus();
//field.value="";
	return false;
}
else
{
	return true;
}
}
//是否為英文大寫
function IsUpper(field)
{
valid="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
for(var i=0;i<field.value.length;i++)
{
temp = "" + field.value.substring(i, i+1);
if (valid.indexOf(temp)<0)
{
alert("請輸入A~Z！");
field.focus();
//field.value="";
	return false;
}else
{
	return true;
}

}
}
//是否為英文小寫
function IsLower(field)
{
valid="abcdefghijklmnopqrstuvwxyz";
for(var i=0;i<field.value.length;i++)
{
temp = "" + field.value.substring(i, i+1);
if (valid.indexOf(temp)<0)
{
alert("請輸入a~z！");
field.focus();
//field.value="";
	return false;
}else
{
	return true;
}

}
}
//是否包含中文
function isChiness(str)
{
	var count = str.length;

	for(var i = 0; i < count; i++)
	{
		if(str.charCodeAt(i) < 0 || str.charCodeAt(i) > 255)
		{
			return true;			
		}
	}
	
	return false;
}
//是否為英數
function isEngOrNum(field)
{
	var count = field.value.length;

	for(var i = 0; i < count; i++)
	{
		var code = parseInt(field.value.charCodeAt(i));

		if((code >= 48 && code <= 57) || (code >= 65 && code <= 90) || (code >= 97 && code <= 122))
		{
			return true;
		}
		else
		{
			alert("請輸入英文字母或是數字!!");
			field.focus();
			return false;
		}
	}
	
	return true;
}
//字串長度
function checkStrLength(str, maxLength)
{
	var count = str.length;
	var strLength = 0;
	
	for(var i = 0; i < count; i++)
	{
		if(str.charCodeAt(i) < 0 || str.charCodeAt(i) > 255)
		{
			strLength += 2
		}
		else
		{
			strLength += 1;
		}
	}
	
	if(strLength <= maxLength)
	{
		return true;
	}
	else
	{
		return false;
	}
}
//身分證驗證
function IsID(field)
{
	if (field.value.length>0)
	{
		var ID_Load = ''+ field.value.toUpperCase()
		//表示為外國人
		if (ID_Load.length==11)
		{
			return true;
		}
		if (ID_Load.length != 10)
		{
				alert('身分證號碼錯誤!\r\n字數不足!')
				field.focus();
				//field.value="";
			return (false)
		}

		//建立一個 ID_Input 陣列
			var ID_Input    = new Array(10)
			//將 ID_Load 字串一個字元接著一個字元放入 ID_Input 陣列內
		for (var i=0; i<10; i++) { ID_Input[i] = ID_Load.charAt(i) }
		//====以下測試 ID_Input[0] 是否為英文字母===
		var EngString = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
		ID_Input[0]   = EngString.indexOf(ID_Input[0])
		if (ID_Input[0] == -1)
		{
			alert('身分證號碼錯誤!\r\n無開頭的字母!')
			field.focus();
			//field.value="";
			return (false)
		}
		if (ID_Input[1] !=1 && ID_Input[1] !=2)
		{
			alert('身分證號碼錯誤!\r\n無法辨識性別!')
			field.focus();
			//field.value="";
			return (false)
		}
		var NumArray  = new Array(26)
		NumArray[0]   = 1 ; NumArray[1]  = 10; NumArray[2]  = 19;
		NumArray[3]   = 28; NumArray[4]  = 37; NumArray[5]  = 46;
		NumArray[6]   = 55; NumArray[7]  = 64; NumArray[8]  = 39;
		NumArray[9]   = 73; NumArray[10] = 82; NumArray[11] = 2 ;
		NumArray[12]  = 11; NumArray[13] = 20; NumArray[14] = 48;
		NumArray[15]  = 29; NumArray[16] = 38; NumArray[17] = 47;
		NumArray[18]  = 56; NumArray[19] = 65; NumArray[20] = 74;
		NumArray[21]  = 83; NumArray[22] = 21; NumArray[23] = 3 ;
		NumArray[24]  = 12; NumArray[25] = 30;
		var result = NumArray[ID_Input[0]]
		for (var i=1; i<10; i++)
		{
			var NumString = '0123456789'
			ID_Input[i] = NumString.indexOf(ID_Input[i])
			if (ID_Input[i] == -1)
			{
				alert('身分證號碼錯誤!\r\n數字檢查錯誤!')
				field.focus();
				//field.value="";
				return (false)
			}
			else
				{ 
					result += ID_Input[i] * (9-i) }
				}

			result += 1 * ID_Input[9]
			if (result % 10 != 0)
			{
				alert('身分證號碼錯誤!\r\n加總檢查錯誤!');
				field.focus();
				//field.value="";
				return (false);
			}
		}
}


//*******統一編號驗證
function IsCompanyID(field){

var cx = new Array;
cx[0] = 1;
cx[1] = 2;
cx[2] = 1;
cx[3] = 2;
cx[4] = 1;
cx[5] = 2;
cx[6] = 4;
cx[7] = 1;


  var NO = field.value;
  var SUM = 0;
  if (NO.length != 8) {
    alert("統編錯誤，要有 8 個數字");
field.focus();
    return;
  }
  var cnum = NO.split("");
  for (i=0; i<=7; i++) {
    if (NO.charCodeAt() < 48 || NO.charCodeAt() > 57) {
      alert("統編錯誤，要有 8 個 0-9 數字組合");
field.focus();
      return;
    }
  
SUM += cc(cnum[i] * cx[i]);

  }
  if (SUM % 10 == 0) 
{return ;	}
  else if (cnum[6] == 7 && (SUM + 1) % 10 == 0)
{return ;}
  else {alert("統一編號："+NO+" 錯誤!");field.focus();}
}

function cc(n){
  if (n > 9) {
    var s = n + "";
    n1 = s.substring(0,1) * 1;
    n2 = s.substring(1,2) * 1;
    n = n1 + n2;
  }
  return n;
}
//*****************

//身分證字號或統一編號驗證
function IsIDorCompanyID(field)
{
	if (field.value.length==8)
	{
		
		IsCompanyID(field);
	}
	else
	{
		IsID(field);
	}
}


//日期判斷,yyyy/mm/dd
function IsDate(field){
	if (field.value.length>0)
	{
		var bolDateValid;
		bolDateValid=true;
		var aryTmp=new Array(3);
		aryTmp=field.value.split("/");
		if(aryTmp.length!=3){
			alert("日期格式不正確");
			bolDateValid=false;
		field.focus();
		//field.value="";	
		return false;
	}
	//BisonLin add 2005/10/27
	//Purpose 判斷日期格式之長度
	if (aryTmp[0].length != 4 || aryTmp[1].length > 2 || aryTmp[2].length > 2)
	{
		var strShow='日期,';
		if (aryTmp[0].length != 4)
		{
		strShow+='年,';
		}
		if (aryTmp[1].length > 2)
		{
		strShow+='月,';
		}
		if (aryTmp[2].length > 2)
		{
		strShow+='日';
		}
		alert(strShow +"格式不正確");
		bolDateValid=false;
		field.focus();
		return false;
	}
	//End added 2005/10/27
   if(isNaN(aryTmp[0]) || isNaN(aryTmp[1]) || isNaN(aryTmp[2]))
   {
      alert("年月日,都必須是數字!");
	  bolDateValid=false;
	  field.focus();
//field.value="";	
	  return false;
   }

   if(aryTmp[0]<1 || aryTmp[0]>2100 || aryTmp[0]<1911)
   {
      alert("日期,年不正確,請重新輸入");
	 bolDateValid=false;
	 field.focus();
//field.value="";	
		return false;
   }

   if(aryTmp[1]<1 || aryTmp[1]>12)
   {
      alert("日期,月不正確,請重新輸入");
      bolDateValid=false;
	 field.focus();
//field.value="";
	return false;
   }


   if(aryTmp[0]%100!=0 && aryTmp[0]%4==0)
   {
      if(aryTmp[1]==1 || aryTmp[1]==3 || aryTmp[1]==5 || aryTmp[1]==7 || 
		aryTmp[1]==8 || aryTmp[1]==10 || aryTmp[1]==12)
		{
         if(aryTmp[2]<1 || aryTmp[2]>31)
         {
            alert("日期,日不正確,請重新輸入");
           bolDateValid=false;
			field.focus();
//field.value="";	
			return false;
         }
      }
      else if(aryTmp[1]==4 || aryTmp[1]==6 || aryTmp[1]==9 || aryTmp[1]==11)
      {
           if(aryTmp[2]<1 || aryTmp[2]>30)
           {
              alert("日期,日不正確,請重新輸入");
            bolDateValid=false;
			field.focus();
//field.value="";	
			return false;
            }
      }
      else{
           if(aryTmp[2]<1 || aryTmp[2]>29)
           {
				alert("日期,日不正確,請重新輸入");
				bolDateValid=false;
				field.focus();
//field.value="";	
				return false;
            }
      }
   }
   else{
     if(aryTmp[1]==1 || aryTmp[1]==3 || aryTmp[1]==5 || aryTmp[1]==7 || 
		aryTmp[1]==8 || aryTmp[1]==10 || aryTmp[1]==12)
		{
			if(aryTmp[2]<1 || aryTmp[2]>31){
			alert("日期,日不正確,請重新輸入");
			bolDateValid=false;
			field.focus();
//field.value="";	
			return false;
         }
      }
   else if(aryTmp[1]==4 || aryTmp[1]==6 || aryTmp[1]==9 || aryTmp[1]==11){
           if(aryTmp[2]<1 || aryTmp[2]>30){
              alert("日期,日不正確,請重新輸入");
             bolDateValid=false;
			field.focus();
//field.value="";	
			return false;
            }
         }
      else{
           if(aryTmp[2]<1 || aryTmp[2]>28){
              alert("日期,日不正確,請重新輸入");
             bolDateValid=false;
field.focus();
//field.value="";	
return false;
            }
      }
   }

   return bolDateValid;
}
}
//日期判斷,yyyy/mm
function IsDate2(field){
if (field.value.length>0)
{
   var bolDateValid;
   bolDateValid=true;
   var aryTmp=new Array(2);
   aryTmp=field.value.split("/");
   if(aryTmp.length!=2){
      alert("日期格式不正確");
      bolDateValid=false;
field.focus();
//field.value="";	
return false;
   }


   if(isNaN(aryTmp[0]) || isNaN(aryTmp[1])){
      alert("年月,都必需是數字!");
     bolDateValid=false;
field.focus();
//field.value="";	
return false;
   }

   if(aryTmp[0]<1 || aryTmp[0]>2100|| aryTmp[0]<1911){
      alert("日期,年不正確,請重新輸入");
     bolDateValid=false;
field.focus();
//field.value="";	
return false;
   }

   if(aryTmp[1]<1 || aryTmp[1]>12){
      alert("日期,月不正確,請重新輸入");
     bolDateValid=false;
field.focus();
//field.value="";
return false;
   }

	if (aryTmp[0].length!=4 || aryTmp[1].length!=2 )
	{
 		alert("日期格式不正確,請重新輸入");
bolDateValid=false;
field.focus();
//field.value="";
return false;
	}

   return bolDateValid;
}
}

//check email
function IsEmail(field){
if (field.value.length>0)
{
var str=field.value
var filter=/^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
if (filter.test(str)){

testresults=true
}
else{
alert("E-MAIL格式驗證錯誤!")
testresults=false
field.focus();
//field.value=""
}
return (testresults)
}
}


function test()
{alert("OK");}

//Validates the input field that only the char
//0:正整數,1:整數,2:包含至小數點兩位之正數,3:包含至小數點兩位之正數
function IsNumber(field,fieldname,fieldtype) {
	if (field.value.length>0)
	{
		var valid="";
		var ok = "yes";
		switch (fieldtype){
			case "0":
				valid="0123456789";
				break;
			case "1":
				valid="0123456789-";
				if ((field.value.indexOf("-")>0) || field.value.split("-").length>2){
					ok="no";

				} 
				break;
			case "2":
				valid="0123456789.";
				if (field.value.indexOf(".")>=0)
				{
					if ((field.value.length-field.value.indexOf("."))>3 || (field.value.indexOf(".")==0)|| (field.value.split(".").length>2)||field.value.indexOf(".")==field.value.length-1)
						{
							ok="no";
						}
				}


				break;
			case "3":
				valid="0123456789.";
				if (field.value.indexOf(".")>=0)
				{
					if ((field.value.length-field.value.indexOf("."))>4 || (field.value.indexOf(".")==0)|| (field.value.split(".").length>2)||field.value.indexOf(".")==field.value.length-1)
					{
						ok="no";
					}
				}
				break;
			case "4":
				valid="0123456789.";
				if (field.value.indexOf(".")>=0)
					{
						if ((field.value.length-field.value.indexOf("."))>5 || (field.value.indexOf(".")==0)|| (field.value.split(".").length>2)||field.value.indexOf(".")==field.value.length-1)
						{
							ok="no";

						}
					}
				break;
		}


		var temp;
		for (var i=0; i<field.value.length; i++) 
		{
			temp = "" + field.value.substring(i, i+1);
			if (valid.indexOf(temp) == "-1") 
			ok = "no";
		}


		if (ok == "no") {
		alert(fieldname + "欄位格式不正確！");
		field.focus();

//field.value="";
}
}
}



//Validates the input field that only the char
function IsNotNumber(field,fieldname) {
if (field.value.length>0)
{
var valid="";
var ok = "yes";


valid="0123456789.";



var temp;
for (var i=0; i<field.value.length; i++) {
temp = "" + field.value.substring(i, i+1);
if (valid.indexOf(temp) != "-1") ok = "no";
}


if (ok == "no") {
alert(fieldname + "欄位格式不正確！");
field.focus();

//field.value="";
}
}
}


//open new window
function OpenNew(page,Winwidth,Winheigth) {
OpenWin = this.open(page, "CtrlWindow", "toolbar=no,scrollbars=yes,location=no,directories=no,status=no,menubar=no,height="+Winheigth+",width="+Winwidth);
}

//Validates the input field that only the char
//包含至小數點下四位、小於一百的正整數
function IsRate(field) {
if (field.value.length>0)
{
	var valid="";
	var ok = "yes";
	
	valid="0123456789.";
	if(parseInt(field.value) > parseInt(99))
	{
		ok="no";
	}
	
	if (field.value.indexOf(".")>=0)
	{

		if ((field.value.length-field.value.indexOf("."))>5 || (field.value.indexOf(".")==0)|| (field.value.split(".").length>2)||field.value.indexOf(".")==field.value.length-1)
		{
			ok="no";
		}
	}

}


var temp;
for (var i=0; i<field.value.length; i++) 
{
	temp = "" + field.value.substring(i, i+1);
	if (valid.indexOf(temp) == "-1") ok = "no";
}

if (ok == "no") 
{
	alert("利率欄位格式不正確！");
	field.focus();

}

}

//Validates the input field that only the char
//包含至小數點下四位、小於一百的正整數
function IsRate(field,fieldname)
{
	if (field.value.length>0)
	{
		var valid="";
		var format = "true";
	
		valid="0123456789.";
		if(parseInt(field.value) > parseInt(99))
		{
			format="false";
		}
	
		if (field.value.indexOf(".")>=0)
		{
			if ((field.value.length-field.value.indexOf("."))>5 || (field.value.indexOf(".")==0)|| (field.value.split(".").length>2)||field.value.indexOf(".")==field.value.length-1)
			{
				format="false";
			}
		}
	}

	var temp;
	for (var i=0; i<field.value.length; i++) 
	{
		temp = "" + field.value.substring(i, i+1);
		if (valid.indexOf(temp) == "-1")
		{
			format = "false";
		}
	}

	if (format == "false") 
	{
		alert(fieldname + "欄位格式不正確！");
		field.focus();
	}	
}

//把檢查日期和時間合起來  格式:yyyy/mm/dd HH:MM by Justin
function IsDateTime(field)
{
	if (field.value.length>0)
	{
		var bolDateValid;
		bolDateValid=true;
		var aryDate=new Array(3);
		var aryDateTime = new Array(2);
		var aryTime = new Array(2);
		aryDateTime = field.value.split(" ");
		aryDate=aryDateTime[0].toString().split("/");
		if(aryDateTime.length != 2)
		{
			alert("格式不正確");
			field.focus();
			bolDateValid=false;
			return false;
		}
		else
		{
			aryTime = aryDateTime[1].toString().split(":");
				
			if(aryDate.length!=3 || aryDateTime.length != 2 || aryTime.length !=2)
			{
				alert("格式不正確");
				bolDateValid=false;
				field.focus();
				return false;
			}
		}
//=====================檢查日期的部份================================
	//Purpose 判斷日期格式之長度
	if (aryDate[0].length != 4 || aryDate[1].length > 2 || aryDate[2].length > 2)
	{
		var strShow='日期,';
		if (aryDate[0].length != 4)
		{
		strShow+='年,';
		}
		if (aryDate[1].length > 2)
		{
		strShow+='月,';
		}
		if (aryDate[2].length > 2)
		{
		strShow+='日';
		}
		alert(strShow +"格式不正確");
		bolDateValid=false;
		field.focus();
		return false;
	}
	//End added 2005/10/27
   if(isNaN(aryDate[0]) || isNaN(aryDate[1]) || isNaN(aryDate[2]))
   {
      alert("年月日,都必須是數字!");
	  bolDateValid=false;
	  field.focus();
	  return false;
   }

   if(aryDate[0]<1 || aryDate[0]>2100 || aryDate[0]<1911)
   {
      alert("日期,年不正確,請重新輸入");
	  bolDateValid=false;
	  field.focus();
	  return false;
   }

   if(aryDate[1]<1 || aryDate[1]>12)
   {
      alert("日期,月不正確,請重新輸入");
      bolDateValid=false;
	  field.focus();
      return false;
   }

   if(aryDate[0]%100!=0 && aryDate[0]%4==0)
   {
      if(aryDate[1]==1 || aryDate[1]==3 || aryDate[1]==5 || aryDate[1]==7 || 
		aryDate[1]==8 || aryDate[1]==10 || aryDate[1]==12)
		{
         if(aryDate[2]<1 || aryDate[2]>31)
         {
            alert("日期,日不正確,請重新輸入");
           bolDateValid=false;
			field.focus();

			return false;
         }
      }
      else if(aryDate[1]==4 || aryDate[1]==6 || aryDate[1]==9 || aryDate[1]==11)
      {
           if(aryDate[2]<1 || aryDate[2]>30)
           {
              alert("日期,日不正確,請重新輸入");
            bolDateValid=false;
			field.focus();
			return false;
            }
      }
      else{
           if(aryDate[2]<1 || aryDate[2]>29)
           {
				alert("日期,日不正確,請重新輸入");
				bolDateValid=false;
				field.focus();
				return false;
            }
      }
   }
   else{
     if(aryDate[1]==1 || aryDate[1]==3 || aryDate[1]==5 || aryDate[1]==7 || 
		aryDate[1]==8 || aryDate[1]==10 || aryDate[1]==12)
		{
			if(aryDate[2]<1 || aryDate[2]>31){
			alert("日期,日不正確,請重新輸入");
			bolDateValid=false;
			field.focus();
			return false;
         }
      }
   else if(aryDate[1]==4 || aryDate[1]==6 || aryDate[1]==9 || aryDate[1]==11){
           if(aryDate[2]<1 || aryDate[2]>30){
              alert("日期,日不正確,請重新輸入");
             bolDateValid=false;
			field.focus();
			return false;
            }
         }
      else{
           if(aryDate[2]<1 || aryDate[2]>28){
              alert("日期,日不正確,請重新輸入");
              bolDateValid=false;
              field.focus();
              return false;
            }
      }
   }

//=====================檢查時間的部份================================
	if (aryTime[0].length != 2 || aryTime[1].length != 2 )
	{
		var strShow1='時間,';
		if (aryTime[0].length != 2)
		{
		strShow+='時,';
		}
		if (aryTime[1].length != 2)
		{
		strShow+='分,';
		}
		alert(strShow +"格式不正確");
		bolDateValid=false;
		field.focus();
		return false;
	}
		
		if(isNaN(aryTime[0]) || isNaN(aryTime[1]))
		{
			alert("時間必需是數字!");
			bolDateValid=false;
			field.focus();
			//field.value="";	
			return false;
		}
		//Bison modified 2005/10/27
		//if(aryDate[0]<0 || aryDate[0]>24)
		if(aryTime[0]<0 || aryTime[0]>24)
		{
			alert("時間HH不正確,請重新輸入00~24");
			bolDateValid=false;
			field.focus();
			//field.value="";	
			return false;
		}
		if(aryTime[1]<0 || aryTime[1]>59)
		{
			alert("時間MM不正確,請重新輸入00~59");
				bolDateValid=false;
				field.focus();
				//field.value="";
				return false;
		}
		}
		
		 return bolDateValid;
}


//str位數(len)不夠時，左補sign
function PadLeft(str,len,sign)
{
	
	if (str.length>=len)
	{
		return str;
	}
	else
	{
		return PadLeft(sign+str,len,sign);
	}

}
//str位數(len)不夠時，右補sign
function PadRight(str,len,sign)
{
	if (str.length>=len)
	{
		return str;
	}
	else
	{
		return PadRight(str+sign,len,sign);
	}

}
///為檢查法辦案號
///BKSUE060~BKSUE070
function checkSuit(obj,intLen,strName)
{
	
	if (isNaN(obj.value))
	{
		obj.focus();
		alert(strName + '必須為數字!\n');
		return false;
	}
	if (obj.value.length==0)
	{
		alert(strName + '不得為空白!\n');
		obj.focus();
		return false;
	}
	if (obj.value.length<intLen)
	{
		var strInput=obj.value;
		
		obj.value=PadLeft(strInput,intLen,'0');
	
		
	}
}