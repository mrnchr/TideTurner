mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");

    window.addEventListener('scroll', function(event) {
      window.scrollTo(0, 0);
      event.preventDefault(); 
    }, false);
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


 SendDataToServer: function (data) {
    var dateString = UTF8ToString(data);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

LoadData: function () {
    player.getData().then (_data => 
    {
      const myJSON = JSON.stringify(_data);
      myGameInstance.SendMessage('YandexGamesIntegration', 'SetData', myJSON);
    });
  },

});