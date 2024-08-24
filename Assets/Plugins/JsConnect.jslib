mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

RateGame : function(){
ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
},


ShowAdv : function(){
		ysdk.adv.showFullscreenAdv({
        callbacks: {
        onClose: function(wasShown) {
				console.log("-------------- closed -------");
        // some action after close
      },
        onError: function(error) {
        // some action on error
		console.log(error);
      }
    }
		})
},


});