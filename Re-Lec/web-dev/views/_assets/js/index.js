$(".search-bar").keypress( function (key) {
	if(key.keyCode == 13) {
		$("#search-form").submit()
	}
});