mergeInto(LibraryManager.library, {
    RateGame: function () {
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

    ShowAd: function () {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function (wasShown) {
                    MyGameInstance.SendMessage("Settings", "UnPause");
                },
                onError: function (error) {
                    // some action on error
                }
            }
        })
    },

    GetLang: function () {
        var lang = ysdk.environment.i18n.lang;
        var buffersize = lengthBytesUTF8(lang) + 1;
        var buffer = _malloc(buffersize);
        stringToUTF8(lang, buffer, buffersize);
        return buffer;
    },

    SaveExtern: function (date) {
        console.log(date);
        player.setStats({ highScore: date });
    },

    LoadExtern: function () {
        player.getStats().then(_date => {
            console.log(_date["highScore"]);
            MyGameInstance.SendMessage("Object", "Load", _date["highScore"]);
        })
    }
});
