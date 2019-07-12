var htmlData;
var escapedHtmlData;
var inputTransition = false;
var selectedTag;
var urlVars = {};
	var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function(m,key,value) {
	urlVars[key] = decodeURIComponent(value);
	});


function Source()
{
	return new Promise((resolve, reject) => {
		var x = urlVars["path"];
		if(autobot.getSource(x)){
			resolve(autobot.getSource(x));
		}
	});
}

Source().then((result) => {
	var outputData = result;
	outputData = outputData.replace(/>/g, ">\n");
	outputData = outputData.replace(/<\//g, "\n</");
	outputData = outputData.replace(/^\s*\n/gm, "");
	htmlData = outputData;
	outputData = outputData.replace(/</g, "&lt;");
	outputData = outputData.replace(/>/g, "&gt;");
	escapedHtmlData = outputData;
	var element = document.getElementById("xmpTagId");
	element.innerHTML = outputData;
	filterTags(htmlData);
});

/*
(async function()
{
	await CefSharp.BindObjectAsync("autobot");
	var x = urlVars["path"];
	autobot.getSource(x).then(function (Result)
	{
		var outputData = Result;
		outputData = outputData.replace(/>/g, ">\n");
		outputData = outputData.replace(/<\//g, "\n</");
		outputData = outputData.replace(/^\s*\n/gm, "");
		htmlData = outputData;
		outputData = outputData.replace(/</g, "&lt;");
		outputData = outputData.replace(/>/g, "&gt;");
		escapedHtmlData = outputData;
		var element = document.getElementById("xmpTagId");
		element.innerHTML = outputData;
		filterTags(htmlData);
	});
})();
*/

function filterTags(data){
	var outputArray = [];
	var tagFilterRegex = /<([a-z]+)\b/g;
	while ((array=tagFilterRegex.exec(data)) !== null) {
		outputArray.push(array[1]);  
	}
	groupFilteredTags(outputArray);
};

function groupFilteredTags(filteredArray){
	var groupedArray = [];
	var countTagObj = {};
	var excludeArray = ['html'];
	if(filteredArray.length === 0){
		return;
	}
	var counter = 0;
	for(var i=0;i<filteredArray.length; i++){
		if(!excludeArray.includes(filteredArray[i])){
			groupedArray.push(filteredArray[i]);
			if(!countTagObj[filteredArray[i]]){
				countTagObj[filteredArray[i]] = {};
				countTagObj[filteredArray[i]].count = 1;
				countTagObj[filteredArray[i]].tagName = '<'+filteredArray[i]+'/>';
			} else {
				countTagObj[filteredArray[i]].count++;
			}
		}
	}
	createDomElements(countTagObj);
};

function createDomElements(countTagObj){
	var tableBody = document.createElement("tbody");
	for (var key in countTagObj){
		var row = document.createElement("tr");
		var column1 = document.createElement("td");
		var column2 = document.createElement("td");
		row.setAttribute("id","row-"+key);
		row.setAttribute('onclick',"selectTag('"+key+"')");
		column1.textContent = key;
		column2.textContent = countTagObj[key].count;
		row.appendChild(column1);
		row.appendChild(column2);
		tableBody.appendChild(row);
	}
	var element = document.getElementById("tag-count-table");
	element.appendChild(tableBody);
};

function selectTag (tag){
	console.log(tag);
	if(selectedTag !== undefined){
		$('#row-'+selectedTag).css('background-color','white');
	}
	selectedTag = tag;
	$('#row-'+tag).css('background-color','#c6e3ff');
	console.log('#row-'+tag);
    var outputData = escapedHtmlData.replace(new RegExp("&lt;"+tag+"&gt;", 'g'), '<mark>$&</mark>');
    var xmp = document.createElement("pre");
    var element = document.getElementById("xmpTagId");
    outputData = outputData.replace(new RegExp("&lt;"+tag+" ", 'g'), '<mark>$&</mark>');
    outputData = outputData.replace(new RegExp("&lt;/"+tag+"&gt;", 'g'), '<mark>$&</mark>');
    xmp.innerHTML = outputData;
    element.innerHTML = outputData;
};