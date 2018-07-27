mergeInto(LibraryManager.library, {
	
	SendData: function(str) {
		window.Drupal.settings.squaresUnityData = Pointer_stringify(str);
		window.onSetSquaresUnityData();
	
	},

});