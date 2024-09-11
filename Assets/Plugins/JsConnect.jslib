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
      ysdk.adv.showFullscreenAdv(
      {
        callbacks: 
        {
            onClose: function(wasShown) 
            {
                console.log("-------------- closed -------");
                    // some action after close
            },
            onError: function(error) 
            {
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
        myGameInstance.SendMessage('YandexGamesIntegration', 'SetNick', player.getName());
    });
},

CheckLoginState:function(){
    if(!player)
    {
        myGameInstance.SendMessage('YandexGamesIntegration', 'CheckAuth', "false");
        return;
    }

    if (player.getMode() === 'lite') 
    {
        myGameInstance.SendMessage('YandexGamesIntegration', 'CheckAuth', "false");
        ysdk.auth.openAuthDialog().then(() => 
        {
            // Игрок успешно авторизован.
            myGameInstance.SendMessage('YandexGamesIntegration', 'CheckAuth', "true");
            InitPlayer().catch(err => 
            {
                // Ошибка при инициализации объекта Player.
                myGameInstance.SendMessage('YandexGamesIntegration', 'CheckAuth', "false");
            });
        }).catch(() => 
        {
            // Игрок не авторизован.
            myGameInstance.SendMessage('YandexGamesIntegration', 'CheckAuth', "false");
        });
    }
},


StartGameplay: function () {
    ysdk.features.GameplayAPI.start();
},

StopGameplay: function () {
    ysdk.features.GameplayAPI.stop();
},

Auth: function() 
{
    if (player.getMode() === 'lite') {
        console.log("Игрок не авторизован.");
        ysdk.auth.openAuthDialog().then(() => {
            console.log("Игрок успешно авторизован.");

            InitPlayer().catch(err => {
                console.log("Ошибка при инициализации объекта Player.");
            });
        }).catch(() => {
            console.log("Игрок не авторизован.");
        });
    }
},

});