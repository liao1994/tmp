// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
var App = App || {};
App.Search = (function () {

	var selectors = {
		imgSearch: ".img-search",
		search: ".search",
		isImgSearch: ".checkbox-img-search",
		searchBar: ".search-bar",
		searchResultContainer: ".search-result"
	};
	function addEventListeners() {
		$(selectors.search).on("click", search);
		$(selectors.imgSearch).on("click", imgSearch);
		$(selectors.isImgSearch).on("click",  searchSwapper);
		
	}
	function initialize() {
		searchSwapper();
		addEventListeners();
	}
	function searchSwapper() {
		if ($(selectors.isImgSearch).is(':checked')) {
			$(selectors.search).hide();
			$(selectors.imgSearch).show();
		
		} else {
			$(selectors.search).show();
			$(selectors.imgSearch).hide();
		}
	}
	function search() {
		var query = $(selectors.searchBar).val();
		$.get("/Home/Search?query=" + query, function (input) {
			$(selectors.searchResultContainer).empty();
			//var result = JSON.parse(input);
			var result = input;
			var html = "<table>";
			for (var i = 0; i < result.data.length; i++) {
				html += "<tr><td>" + result.data[i].text + "</td></tr>";
			}
			html += "</table>";
			$(selectors.searchResultContainer).html(html);
	});
	}
	function imgSearch() {
		var query = $(selectors.searchBar).val();
			$.get("/Home/SearchImages?query=" + query, function (input) {
			$(selectors.searchResultContainer).empty();
			var result = input;
			var html = "<table>";
				for (var i = 0; i < result.includes.media.length; i++) {
					html += "<tr><td><img src='" + result.includes.media[i].url.replace("https://pbs.twimg.com","Home") + "'/></td></tr>";
				}
			html += "</table>";
			$(selectors.searchResultContainer).html(html);
		});
	}
	return {
		initialize: initialize
	};
})();

// Write your JavaScript code.
$(document).ready(function() {
	App.Search.initialize();
});