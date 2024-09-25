mergeInto(LibraryManager.library, 
{
    RateGame : function(){
        ysdk.feedback.canReview()
        .then(({ value, reason }) => 
        {
            if (value) 
            {
                ysdk.feedback.requestReview()
                .then(({ feedbackSent }) => 
                {
                    console.log(feedbackSent);
                })
            } else 
            {
                console.log(reason)
            }
        })
    },


    ShowAdv : function()
    {
        console.log("Start Adv");
        ysdk.adv.showFullscreenAdv(
        {
            callbacks: 
            {
                onClose: function(wasShown) 
                {
                    console.log("-------------- closed -------");
                    // some action after close
                    myGameInstance.SendMessage('YandexGamesIntegration', 'AdvFinished');
                },
                onError: function(error) 
                {
                    // some action on error
                    console.log(error);
                    myGameInstance.SendMessage('YandexGamesIntegration', 'AdvFinished');
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
            myGameInstance.SendMessage('YandexGamesIntegration', 'SetNick', player.getName());
        });
    },

    LoadApiReady: function () {
        ysdk.features.LoadingAPI.ready();
    },


    StartGameplay: function () {
        ysdk.features.GameplayAPI.start();
    },

    StopGameplay: function () {
        ysdk.features.GameplayAPI.stop();
    },

    Auth: function() 
    {
        initPlayer().then(_player =>
        { 
            player = _player

            if (player.getMode() === 'lite') {
                console.log("Игрок не авторизован.");
                ysdk.auth.openAuthDialog().then(() => {
                    console.log("Игрок успешно авторизован.");
                    myGameInstance.SendMessage('YandexGamesIntegration', 'CheckAuth', true);
                }).catch(() => {
                    myGameInstance.SendMessage('YandexGamesIntegration', 'CheckAuth', false);
                });
            }else{
                myGameInstance.SendMessage('YandexGamesIntegration', 'CheckAuth', true);
            }
        });
    },

    Language: function(){
        console.log("ysdk.environment.i18n.lang: " + ysdk.environment.i18n.lang);
        myGameInstance.SendMessage('YandexGamesIntegration', 'SetLanguage', ysdk.environment.i18n.lang);
    },

});