var $container;
var timer;

$(document).ready(function () {
  $container = $("#downloads-container");
  UpdateList();
  timer = setInterval(UpdateList, 500);
});

function formatBytes(bytes, decimals) {
  if (bytes == 0) return '0 Byte';
  var k = 1000;
  var dm = decimals + 1 || 3;
  var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
  var i = Math.floor(Math.log(bytes) / Math.log(k));
  return (bytes / Math.pow(k, i)).toPrecision(dm) + ' ' + sizes[i];
}

String.prototype.between = function (prefix, suffix) {
	s = this;
	var i = s.indexOf(prefix);
	if (i >= 0) {
		  s = s.substring(i + prefix.length);
	}
	else {
		return '';
	}
	if (suffix) {
		i = s.indexOf(suffix);
		if (i >= 0) {
			s = s.substring(0, i);
		}
		else {
			return '';
		}
	}
	return s;
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

function getDate(dt) {
	return eval("new " + dt.between("/","/"));
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

function setIkon(name){
	var x, ext ;
	ext = name.split('.').pop();
	if(ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "gif" || ext == "icon") x = "fa-image";
	else if(ext == "mp4") x = "fa-file-video-o";
	else if(ext == "mp3") x = "fa-music";
	else if(ext == "rar" || ext == "zip") x = "fa-file-archive-o";
	else if(ext == "txt") x = "fa-file-text";
	else if(ext == "js" || ext == "php" ) x = "fa-file-code-o";
	else if(ext == "doc" || ext == "docx" ) x = "fa-file-word-o";
	else if(ext == "ppt" || ext == "pptx" ) x = "fa-file-powerpoint-o";
	else if(ext == "pdf") x = "fa-file-pdf-o";
	else{ 
		x = "fa-file";
	}
	return x;
}

function UpdateItem(item) {
	var h  = ["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];
	var startTime = Date.parse(item.StartTime);
	var d = new Date(startTime);
	var sejak = getTime(startTime);
	var icon = $("#ikon");
	
	var progress = "";
	var $template = $("#template").find("#download").clone();
	var $since = $container.find(".time-label:contains("+ sejak +")");
	var id = "#d" + item.Id;
	
	if($since.length == 0) {
		var xx = '<li id="since" class="time-label"><span class="bg-aqua">' + sejak + '</span></li>';
		$container.append(xx);
	}
	
	if ($container.find(".timeline-item:contains("+ item.SuggestedFileName +")").length == 0) {
		$template.attr("id", id);
		
		
		$template.find("#ikon1").attr("class", "fa " + setIkon(item.SuggestedFileName) + " bg-blue");
		$template.find("#ikon2").attr("class", "fa " + setIkon(item.SuggestedFileName));
		
		if (item.SuggestedFileName != "") $template.find("#filename").text(item.SuggestedFileName);
		
		$template.find("#links").html('<a href="' + item.Url + '" target="_blank" focus-type="url">' + item.Url + '</a>');
		$template.find("#tgl").html('<i class="fa fa-clock-o"></i> ' + h[d.getDay()] +" "+ d.getDay() +", "+ d.getFullYear() +" "+ format(startTime, "hh:mm:ss"));
		
		if (item.IsInProgress) {
			progress = formatBytes(item.CurrentSpeed) + "/s - " + formatBytes(item.ReceivedBytes, 2);
			if (item.TotalBytes > 0) progress += " of " + formatBytes(item.TotalBytes, 2);
			if (item.PercentComplete > 0) progress += " (" + item.PercentComplete + "%)";
			$template.find(".progress-bar").css("width" , item.PercentComplete + "%");
			
			$template.find("a#stop").click(function () {
				autobot.stopDownload(item.Id);
			});
			$template.find("a#batal").click(function () {
				autobot.cancelDownload(item.Id);
			});
			$template.find("#opendir").remove();
			$template.find("#retry").remove();
			$template.find("#terus").remove();
			$template.find("#hps").remove();
		}else if (item.IsCancelled) {
			progress = "Cancelled";
			$template.find("a#retry").click(function () {
				autobot.retryDownload(item.Id);
			});
			$template.find("#opendir").remove();
			$template.find("#stop").remove();
			$template.find("#terus").remove();
			$template.find("#hps").remove();
			$template.find("#batal").remove();
		}
		else if(item.IsComplete){
			progress = "Complete";
			$template.find("a#opendir").click(function () {
				autobot.opendirDownload(item.FullPath);
			});
			$template.find("a#hps").click(function () {
				autobot.deleteDownload(item.Id);
			});
			$template.find(".progress-bar").css("width" , "100%");
			$template.find("#stop").remove();
			$template.find("#retry").remove();
			$template.find("#terus").remove();
			$template.find("#batal").remove();
		}else{
			$template.find("a#terus").click(function () {
				autobot.resumeDownload(item.Id);
			});
		}
		$template.find("#status").text(progress);
		$template.insertAfter($since);
	}
}

function UpdateList() {
	autobot.getDownloads().then(function (res) {
		var list = JSON.parse(res);
		$.each(list, function (key, item) {
			UpdateItem(item.v);
		});
		if($container.find(".end").length == 0 ){
			var x = '<li class="end"><i class="fa fa-clock-o bg-green"></i></li>';
			$container.append(x);
		}
  });
}