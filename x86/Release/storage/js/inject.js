if(typeof jQuery=='undefined')
{
    var headTag = document.getElementsByTagName("head")[0];
    var jqTag = document.createElement('script');
    jqTag.type = 'text/javascript';
    jqTag.src = 'pelitabangsa://browser/js/jquery.min.js';
    headTag.appendChild(jqTag);
	
    var botTag = document.createElement('script');
	botTag.type = 'text/javascript';
    botTag.src = 'pelitabangsa://browser/js/autobot.js';
    headTag.appendChild(botTag);
}else{
	$(document).ready(function (){ $('head').append('<script type="text/javascript" src="pelitabangsa://browser/js/autobot.js"></script>'); });
}