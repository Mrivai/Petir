var $container;
var timer;

$(document).ready(function () {
	
	$container = $("#history-container");
	UpdateList();
	timer = setInterval(UpdateList, 1000);
});

function UpdateList() {
	autobot.getHistory().then(function (res) {
		var list = JSON.parse(res);
		$.each(list, function (key, item) {
			UpdateItem(item);
		});
		
		if($container.find(".end").length == 0 ){
			var x = '<li class="end"><i class="fa fa-clock-o bg-green"></i></li>';
			$container.append(x);
		}
		$('input[type="checkbox"].flat-red').iCheck({
			checkboxClass: 'icheckbox_flat-green'
		});
	});
}

function UpdateItem(item) {
	var h  = ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];
	var startTime = Date.parse(item.Tanggal);
	var d = new Date(startTime);
	var sejak = getTime(startTime);
	
	var $template = $("#template").find(".history-item").clone();
	var $since = $container.find(".time-label:contains("+ sejak +")");
	var id = "H-" + item.Id;
	
	if($since.length == 0) {
		var timeLabel = '<li id="since" class="time-label"><span class="bg-aqua">' + sejak + '</span></li>';
		$container.append(timeLabel);
	}
	if ($container.find(".history-item[id='"+ id +"']").length == 0) {
		$template.attr("id", id);
		$template.find(".history-label").html('<input type="checkbox" class="flat-red" value="' + item.Id + '">');
		$template.find(".timeline-header").html('<a href="' + item.Url + '" target="_blank" focus-type="url">' + item.Title + '</a> ' + item.Url);
		$template.find("#tgl").html('<i class="fa fa-clock-o"></i> ' + h[d.getDay()] +" "+ d.getDay() +", "+ d.getFullYear() +" "+ format(startTime, "hh:mm:ss"));
		$template.insertAfter($since);
	}
}

function getTime(dt) {
	var today, someday, text;
	today = new Date();
	someday = new Date(dt);
	if (someday.getFullYear() < today.getFullYear()) {
		text = "Last Year";
	}
	if(someday.getFullYear() == today.getFullYear() && someday.getMonth() < today.getMonth()){
		text = today.getMonth() - someday.getMonth() + 1 + "Month Ago";
	}
	if(someday.getFullYear() == today.getFullYear() && someday.getMonth() == today.getMonth()){
		text = getWeek(today.getDate(), someday.getDate());
	}
	return text;
}

function getWeek(x, y) {
	var z, t;
	t = x - y / 7;
	if(x - y == 0){
		z = "Today";
	}else if(x - y <= 7){
		z = x - y + " Days Ago";
	}
	else{
		if(t >= 3){
			z = "4 Week Ago";
		}
		if(t >= 2){
			z = "3 Week Ago";
		}
		if(t >= 1 ){
			z = "2 Week Ago";
		}
	}
	return z;
}

function format(time, format){
	var dt = new Date(time);
	var o = {
		"M+": dt.getMonth() + 1, //month
		"d+": dt.getDate(),    //day
		"h+": dt.getHours(),   //hour
		"m+": dt.getMinutes(), //minute
		"s+": dt.getSeconds(), //second
		"q+": Math.floor((dt.getMonth() + 3) / 3),  //quarter
		"S": dt.getMilliseconds() //millisecond
	}

	if (/(y+)/.test(format)) format = format.replace(RegExp.$1,(dt.getFullYear() + "").substr(4 - RegExp.$1.length));
	for (var k in o) if (new RegExp("(" + k + ")").test(format))
		format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] :("00" + o[k]).substr(("" + o[k]).length));
	return format;
}



