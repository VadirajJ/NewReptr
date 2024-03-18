function isDateValid(dtStr, sformat) {
    // 1 = dd/mm/yyyy , 2=mm/dd/yyyy ,3=yyyy/mm/dd
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var sdateformat
    var strYr

    if (sformat == 1) {
        var strDay = dtStr.substring(0, pos1)
        var strMonth = dtStr.substring(pos1 + 1, pos2)
        var strYear = dtStr.substring(pos2 + 1)
        strYr = strYear
        sdateformat = "dd/mm/yyyy"

    }
    if (sformat == 2) {
        var strMonth = dtStr.substring(0, pos1)
        var strDay = dtStr.substring(pos1 + 1, pos2)
        var strYear = dtStr.substring(pos2 + 1)
        strYr = strYear
        sdateformat = "mm/dd/yyyy"
    }
    if (sformat == 3) {
        var strYear = dtStr.substring(0, pos1)
        var strMonth = dtStr.substring(pos1 + 1, pos2)
        var strDay = dtStr.substring(pos2 + 1)
        strYr = strYear
        sdateformat = "yyyy/mm/dd"
    }

    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1)
            strYr = strYr.substring(1)
    }

    if ((strMonth == "08") || (strMonth == "09")) {
        month = parseInt(strMonth, 10)
        //alert("Month: "+ month)
    }

    if ((strMonth != "08") && (strMonth != "09")) {
        month = parseInt(strMonth)
        //alert("!Month: "+ month)
    }

    if ((strDay == "08") || (strDay == "09")) {
        day = parseInt(strDay, 10)
        //alert("Day: "+ day)
    }

    if ((strDay != "08") && (strDay != "09")) {
        day = parseInt(strDay)
        //alert("!Day: "+ day)
    }
    //day=parseInt(strDay)
    //month=parseInt(strMonth)
    year = parseInt(strYr)

    if (pos1 == -1 || pos2 == -1) {
        alert("The date format should be : " + sdateformat)
        return false
    }
    if (strMonth.length <= 1 || (month < 1 || month > 12)) {
        alert("Please enter a valid month: " + sdateformat)
        return false
    }
    if (strDay.length <= 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        alert("Please enter a valid day: " + sdateformat)
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        alert("Please enter a valid date: " + sdateformat)
        return false
    }
    return true
}


function validateStrongPassword(fieldvalue) {
    //Initialise variables
    var space = " ";

    //It must not contain a space
    if (fieldvalue.indexOf(space) > -1) {
        alert("Passwords cannot include a space")
        return false
    }

    //It must contain at least one number character
    if (!(fieldvalue.match(/\d/))) {
        alert("Strong Passwords must include at least one number")
        return false;
    }

    //It must start with at least one letter     
    if (!(fieldvalue.match(/^[a-zA-Z]+/))) {
        alert("Strong Passwords must start with at least one letter")
        return false;
    }

    //It must contain at least one special character
    if (!(fieldvalue.match(/\W+/))) {
        alert("Strong Passwords must include at least one special character - #,@,%,!")
        return false;
    }
    return true;
}


/* DHTML date validation script for dd/mm/yyyy. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)*/
/// Declaring valid date character, minimum year and maximum year
var dtCh = "/";
var minYear = 1900;
var maxYear = 2100;
function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}
function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}
function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}
function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}

function isIntegerNew(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (c != ".") {
            if (((c < "0") || (c > "9"))) return false;
        }
    }
    return true;
}


//			function isIntegerNew(s)
//			{
//			var i;
//			for (i = 0; i < s.length; i++)
//			 {   
//				// Check that current character is number.
//				var c = s.charAt(i);
//				
//				if (c!=".")
//				{
//				if (((c < "0") || (c > "9") )) return false;
//				}
//			 }
//			}

/*	function isDate(dtStr)
{		
var daysInMonth = DaysArray(12)
var pos1=dtStr.indexOf(dtCh)
var pos2=dtStr.indexOf(dtCh,pos1+1)
var strDay=dtStr.substring(0,pos1)
var strMonth=dtStr.substring(pos1+1,pos2)
var strYear=dtStr.substring(pos2+1)
strYr=strYear
if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
for (var i = 1; i <= 3; i++) 
{
if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
}
month=parseInt(strMonth)
day=parseInt(strDay)
year=parseInt(strYr)
if (pos1==-1 || pos2==-1)
{
alert("The date format should be : dd/mm/yyyy")
return false
}
			
if (strMonth.length<1 || month<1 || month>12)
{
alert("Please enter a valid month")
return false
}
if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month])
{
alert("Please enter a valid day")
return false
}
if (strYear.length != 4 || year==0 || year<minYear || year>maxYear)
{
alert("Please enter a valid 4 digit year between "+minYear+" and "+maxYear)
return false
}
if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false)
{
alert("Please enter a valid date")
return false
}
return true
}*/
function isDate(dtStr) {
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strDay = dtStr.substring(0, pos1)
    var strMonth = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    //			alert(strDay);
    if (strDay.length != 2) {
        alert("The date field should be in dd format");
        return false;
    }
    if (strMonth.length != 2) {
        alert("The month field should be in mm format");
        return false;
    }

    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (pos1 == -1 || pos2 == -1) {
        alert("The date format should be : dd/mm/yyyy")
        return false
    }

    if (strMonth.length < 1 || month < 1 || month > 12) {
        alert("Please enter a valid month")
        return false
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        alert("Please enter a valid day")
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        alert("Please enter a valid date")
        return false
    }
    return true
}


function ValidateFormDate(sDate) {
    var dt = sDate
    if (isDate(dt) == false) {
        return false
    }
    return true
}


//checking for spacebar,dot and Enter key also
function alphanumeric(param) {
    var str, words, txt = "";
    //Decode the escape characters to check for 'Enter' key i.e. %0D%0A
    str = escape(param)
    if (str.indexOf("%0D%0A")) {
        //Split into an array of words with "%0D%0A" as the delimiter
        words = str.split("%0D%0A")
        for (var i = 0; i < words.length; i++) {
            txt = txt.concat(words[i]);
        }
        //Encode other escape characters back
        str = unescape(txt)
    }
    else
    //Encode the escape characters back if enter was not pressed
        str = unescape(param)

    for (var i = 0; i < str.length; i++) {
        var chr = str.charAt(i);
        var code = chr.charCodeAt(0);
        //if((code>47 && code<58) || (code>64 && code<91) || (code>96 && code<123) ||(code==32)||(code==46))
        if (code == 39)
            return false;
        else
            continue;
    }
    return true;
    /*var n = param;
    for(var j=0; j<n.length; j++)
    {
    var str = n.charAt(j);
    var chr = str.charCodeAt(0);
				
    if((chr>47 && chr<58) || (chr>64 && chr<91) || (chr>96 && chr<123) ||(chr==32)||(chr==46))
    continue;
    else
    return false;
    }
    return true;*/
}

//checking for (spacebar,dot) also
function alpha(param) {
    var n = param;
    for (var j = 0; j < n.length; j++) {
        var str = n.charAt(j);
        var chr = str.charCodeAt(0);
        if ((chr > 64 && chr < 91) || (chr > 96 && chr < 123) || (chr == 32) || (chr == 46))
        //if((chr==39)
            continue;
        else
            return false;
    }
    return true;
}
function OnlyNumber(param) {
    var n = param;
    for (var j = 0; j < n.length; j++) {
        var str = n.charAt(j);
        var chr = str.charCodeAt(0);
        if ((chr > 47 && chr < 59))
            continue;
        else
            return false;
    }
    return true;
}
function CorrectEmail(param) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (filter.test(param))
        return true;
    else
        return false;
}

function CorrectEmailMultiple(param) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var mySplitResult = param.split(";");
    for (i = 0; i < mySplitResult.length; i++) {
        if (filter.test(mySplitResult[i])) {
        }
        else {
            return false;
        }
    }
    return true;
}

function PortValidation(param) {
    var filter =  /^\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}$/;
    if (filter.test(param))
        return true;
    else
        return false;
}

function CheckDecimal(param) 
{ 
    var decmal= /^\d+(\.\d{1,5})+$/;
        if(decmal.test(param)) 
        return true;
    var decmal=/^\d+$/;
    if(decmal.test(param))
      return true;
      
    else 
    
    return false;
 
  }
    



function isValidPhoneNumber(param) {
    var str = param
    for (var i = 0; i < str.length; i++) {
        var chr = str.charAt(i);
        var code = chr.charCodeAt(0);
        if ((code > 47 && code < 58) || (code == 32) || (code == 40) || (code == 41) || (code == 45))
            continue;
        else
            return false;
    }
    return true;
}

//checking for spacebar,dot,coma and Enter key also
function isValidAddress(param) {
    var str, words, txt = "";
    //Decode the escape characters to check for 'Enter' key i.e. %0D%0A
    str = escape(param)
    if (str.indexOf("%0D%0A")) {
        //Split into an array of words with "%0D%0A" as the delimiter
        words = str.split("%0D%0A")
        for (var i = 0; i < words.length; i++) {
            txt = txt.concat(words[i]);
        }
        //Encode other escape characters back
        str = unescape(txt)
    }
    else
    //Encode the escape characters back if enter was not pressed
        str = unescape(param)

    for (var i = 0; i < str.length; i++) {
        var chr = str.charAt(i);
        var code = chr.charCodeAt(0);
        if ((code > 47 && code < 58) || (code > 64 && code < 91) || (code > 96 && code < 123) || (code == 32) || (code == 35) || (code >= 40 && code <= 47))
            continue;
        else {
            //alert(code);
            return false;
        }
    }
    return true;
}
function Rangvalidation(p) {
    if (p > 100 && p < 1000)
        return true;
    else
        return false;
}
function isBigger(param1, param2) {

    var num1 = param1
    var num2 = param2

    if (num2 > num1) {
        return false;
    }
    else
        return true;
}
function isValidText(param) {

    var str = param;
    for (var i = 0; i < 2; i++) {
        var m = str.charAt(i);
        var chr = m.charCodeAt(0);
        if (chr !== 32)
            continue;
        else
            return false;
    }
    return true;
}
function isValidCurrency(param) {
    var n = param;
    for (var j = 0; j < n.length; j++) {
        var str = n.charAt(j);
        var chr = str.charCodeAt(0);
        if ((chr > 47 && chr < 58) || (chr == 44) || (chr == 46) || (chr == 32))
            continue;
        else
            return false;
    }
    return true;
}
function GetCode(param) {
    var num = param;
    for (var i = 0; i < num.length; i++) {
        var ch = num.charAt(i);
        var code = ch.charCodeAt(0);
        if (code == 33 || code == 35 || code == 37 || code == 38 || code == 42 || code == 94 || code == 36 || code == 40 || code == 41) {
            return false;
        }
    }
    return true;
}

function FieldSize(param) {
    var num = param;
    var n = num.length;
    return n;
}
function Rangvalidation(p) {
    if (p > 100 && p < 1000)
        return true;
    else
        return false;
}

function Checkrange(param) {
    if (param > 0 && param < 10)
    //alert(param)
    //if ((param!==0)||(param!==1)||(param!==2)||(param!==3)||(param!==4)||(param!==5)||(param!==6)||(param!==7)||(param!==8)||(param!==9))					
        return true;
    else
        return false;
}

function CheckMonthRange(param) {
    if (param > 0 && param <= 12)
        return true;
    else
        return false;
}

function DisableRightClick() {
    if (event.button == 2) {
        alert("Sorry no rightclick on this page.")
    }
}

function CompardDates(dtFrom, dtTo)   //This is to compare 2 dates , if 1 then first DAte is Grater if 2 then 2dn date is Grater
{
    var dtCh = "/";
    var pos1 = dtFrom.indexOf(dtCh)
    var pos2 = dtFrom.indexOf(dtCh, pos1 + 1)
    var strDay = dtFrom.substring(0, pos1)
    //If Day is single digit Insert 0 before the Numbert.
    if (strDay.length == 1)
        strDay = "0" + strDay
    var strMonth = dtFrom.substring(pos1 + 1, pos2)
    //If Month is single digit Insert 0 before the Numbert.
    if (strMonth.length == 1)
        strMonth = "0" + strMonth
    var strYear = dtFrom.substring(pos2 + 1)
    var DateTwoPos1 = dtTo.indexOf(dtCh)
    var DateTwoPos2 = dtTo.indexOf(dtCh, DateTwoPos1 + 1)
    var strDay1 = dtTo.substring(0, DateTwoPos1)
    //If Day is single digit Insert 0 before the Numbert.
    if (strDay1.length == 1)
        strDay1 = "0" + strDay1
    var strMonth1 = dtTo.substring(DateTwoPos1 + 1, DateTwoPos2)
    //If Month is single digit Insert 0 before the Numbert.
    if (strMonth1.length == 1)
        strMonth1 = "0" + strMonth1
    var strYear1 = dtTo.substring(DateTwoPos2 + 1)
    if (strYear > strYear1)
        return 1
    else
        if (strYear < strYear1)
        return 2
    else
        if (strMonth > strMonth1)
        return 1
    else
        if (strMonth < strMonth1)
        return 2
    else
        if (strDay > strDay1)
        return 1
    else
        if (strDay < strDay1)
        return 2
    else
        if (strDay == strDay1)
        return 0


}

function Trim(TRIM_VALUE) {
    if (TRIM_VALUE.length < 1) {
        return "";
    }
    TRIM_VALUE = RTrim(TRIM_VALUE);
    TRIM_VALUE = LTrim(TRIM_VALUE);
    if (TRIM_VALUE == "") {
        return "";
    }
    else {
        return TRIM_VALUE;
    }
} //End Function

function RTrim(VALUE) {
    var w_space = String.fromCharCode(32);
    var v_length = VALUE.length;
    var strTemp = "";
    if (v_length < 0) {
        return "";
    }
    var iTemp = v_length - 1;
    while (iTemp > -1) {
        if (VALUE.charAt(iTemp) == w_space) {
        }
        else {
            strTemp = VALUE.substring(0, iTemp + 1);
            break;
        }
        iTemp = iTemp - 1;

    } //End While
    return strTemp;

} //End Function

function LTrim(VALUE) {
    var w_space = String.fromCharCode(32);
    if (v_length < 1) {
        return "";
    }
    var v_length = VALUE.length;
    var strTemp = "";

    var iTemp = 0;

    while (iTemp < v_length) {
        if (VALUE.charAt(iTemp) == w_space) {
        }
        else {
            strTemp = VALUE.substring(iTemp, v_length);
            break;
        }
        iTemp = iTemp + 1;
    } //End While
    return strTemp;
} //End Function

function GreaterThanZero(param) {
    if (param == 0 || param < 0) {
        return false;
    }
    else {
        return true;
    }
}


function DataValid(D1, D2) {
    var dtCh = "/";
    var pos1 = D1.indexOf(dtCh)
    var pos2 = D1.indexOf(dtCh, pos1 + 1)
    var strDay = D1.substring(0, pos1)
    //If Day is single digit Insert 0 before the Numbert.
    if (strDay.length == 1)
        strDay = "0" + strDay
    var strMonth = D1.substring(pos1 + 1, pos2)
    //If Month is single digit Insert 0 before the Numbert.
    if (strMonth.length == 1)
        strMonth = "0" + strMonth
    var strYear = D1.substring(pos2 + 1)
    var DateTwoPos1 = D2.indexOf(dtCh)
    var DateTwoPos2 = D2.indexOf(dtCh, DateTwoPos1 + 1)
    var strDay1 = D2.substring(0, DateTwoPos1)
    //If Day is single digit Insert 0 before the Numbert.
    if (strDay1.length == 1)
        strDay1 = "0" + strDay1
    var strMonth1 = D2.substring(DateTwoPos1 + 1, DateTwoPos2)
    //If Month is single digit Insert 0 before the Numbert.
    if (strMonth1.length == 1)
        strMonth1 = "0" + strMonth1
    var strYear1 = D2.substring(DateTwoPos2 + 1)
    if (strYear > strYear1)
        return false
    else
        if (strYear < strYear1)
        return true
    else
        if (strMonth > strMonth1)
        return false
    else
        if (strMonth < strMonth1)
        return true
    else
        if (strDay > strDay1)
        return false
    else
        if (strDay < strDay1)
        return true
    else
        if (strDay == strDay1)
        return true
}



function ValidateListBox(lstbox) {

    var ListBox = lstbox;
    var length = ListBox.length;
    var i = 0;
    var SelectedItemCount = 0;

    for (i = 0; i < length; i++) {
        if (ListBox.options[i].selected) {
            SelectedItemCount = SelectedItemCount + 1;
        }
    }
    return SelectedItemCount;
}