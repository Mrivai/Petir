
function getXpath(element){
	return "//" + $(element).parents().andSelf().map(function () {
		var $this = $(this);
		var tagName = this.nodeName;
		if ($this.siblings(tagName).length > 0) {
			tagName += "[" + ($this.prevAll(tagName).length + 1 ) + "]";
		}
		return tagName;
	}).get().join("/").toLowerCase();
}

function Showxpath()
{
	$(document).ready(function (){ $('body').append('<div id="xpath-wrap" style="pointer-events:none;top:0;position:absolute;z-index:10000000;"><canvas id="xpath-canvas" style="position:relative;"></canvas></div><div id="xpath-content" style="bottom:0;left:0;cursor:initial!important;padding:10px;background:gray;color:white;position:fixed;font-size:14px;z-index:10000000;"></div>'); });
}

function Clearxpath()
{
	$("#xpath-wrap").remove();
	$("#xpath-content").remove();
}

function inject()
{
	/*
	// append xpath function
	// $(document).ready(function (){ $('head').append('<style type="text/css">.tanda{ border: 1px solid red } .tanda-clicked{ border: 1px solid green }</style>'); });
	// $(document).ready(function (){ $('head').append('<style id="xpath-css">* {cursor:crosshair!important;} #xpath-wrap {pointer-events:none;top:0;position:absolute;z-index:10000000;} #xpath-content {bottom:0;left:0;cursor:initial!important;padding:10px;background:gray;color:white;position:fixed;font-size:14px;z-index:10000000;} #xpath-canvas {position:relative;}</style>'); });
	$(document).ready(function (){ $('body').append('<div id="xpath-wrap" style="pointer-events:none;top:0;position:absolute;z-index:10000000;"><canvas id="xpath-canvas" style="position:relative;"></canvas></div><div id="xpath-content" style="bottom:0;left:0;cursor:initial!important;padding:10px;background:gray;color:white;position:fixed;font-size:14px;z-index:10000000;"></div>'); });
	$('body').mouseover(function (event) {
		const contentNode = document.getElementById('xpath-content');
		if (contentNode) 
		{
			contentNode.innerText = getXpath(event.target);
		}
	});
	$('body').mouseout(function(){
		$("xpath-wrap").remove();
	})
	
	//$(document).ready(function (){ $('head').append('<style id="xpath-css"> #xpath-wrap {pointer-events:none;top:0;position:absolute;z-index:10000000;} #xpath-content {bottom:0;left:0;cursor:initial!important;padding:10px;background:gray;color:white;position:fixed;font-size:14px;z-index:10000000;} #xpath-canvas {position:relative;}</style>'); });
	//$(document).ready(function (){ $('body').append('<div id="xpath-wrap" style="pointer-events:none;top:0;position:absolute;z-index:10000000;"><canvas id="xpath-canvas" style="position:relative;"></canvas></div><div id="xpath-content" style="bottom:0;left:0;cursor:initial!important;padding:10px;background:gray;color:white;position:fixed;font-size:14px;z-index:10000000;"></div>'); });
	
	$('body').mouseover(function (event) {
		Showxpath();
		const contentNode = document.getElementById('xpath-content');
		if (contentNode) 
		{
			contentNode.innerText = getXpath(event.target);
		}
	});
	$('body').mouseout(function(){
		Clearxpath();
	});
	*/
	$('body').on('mouseenter','a', function(){
		Showxpath();
		const contentNode = document.getElementById('xpath-content');
		if (contentNode) 
		{
			contentNode.innerText = this.href;
		}
	});
	$('body').on('mouseleave','a', function(){
		Clearxpath();
	});
	/*
	// check if form exist, star form autofill
	// below is autofill function
	*/
	var form = $('form').length;
	if(form !=null)
	{
		$('form').change(function (x) {
			var xtag = x.target.localName;
			var xtype = x.target.type;
			var xname = x.target.name;
			var xvalue = x.target.value;
			autobot.addcommand(xtag, xtype, xname, xvalue);
		})
		$('[type="submit"]').click(function () {
			autobot.submit();
		})
	}
	/*
	// autofill end
	*/
}

function MonkeyMenu(){
	$(document).ready(function (){ $('body').append('<menu id="monkeymenu" type="context"></menu>'); });
	$(document).ready(function (){ $('body').append('<span style="bottom: 13px; right: 16px;position: fixed;" class="context-menu-monkey">Monkey</span>'); });
	script = document.createElement('script');
    script.type = 'text/javascript';
    script.text = "$(function(){ $.contextMenu({ selector: '.context-menu-monkey',  items: $.contextMenu.fromMenu($('#monkeymenu'))    }); });";
    document.getElementsByTagName('body')[0].appendChild(script);
}

function MonkeyMenuItem(){
	//ini item monkey menu
	//$(document).ready(function (){ $('#monkeymenu').append('<command label="resize" onclick="alert('ini fungsi yang dipanggil')"></command>'); });
}

function go(x)
{
	var u = document.URL;
	if(x != u)
	{
		autobot.go(x);
	}
}
function Fill(x,z)
{
	autobot.fill(x,z);
}
function Write(x,z)
{
	autobot.write(x,z);
}
function Select(x,z)
{
	autobot.select(x,z);
}
function Check(x,z)
{
	autobot.check(x,z);
}
function Upload(z)
{
	$('input[type="file"]').click();
	setTimeout(selectfile(z), 3000);
	clearTimeout();
}
function selectfile(z)
{
	autobot.SelectFile(z);
}
function Submit()
{
	$('[type="submit"]').click();
}

inject();