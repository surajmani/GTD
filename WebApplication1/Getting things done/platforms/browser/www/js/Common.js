/// <reference path="jquery-1.10.2.min.js" />
var baseUrl = 'http://192.168.43.73/';
var UserID = '';
var Common = {
    printText: function (elem, text) {
        $(elem).html(text);
        console.log(text);

    },
    beforeSend: function () {
        var header = {};
        //header.Authorization = 'Bearer ' + login.logData.ClientSecret;
        return header;
    },
    Post: function (dataTopost, url, callBack, showprg) {
        $.support.cors = true;
        if (showprg == null || showprg == true) {
            Common.ShwoprogressModal();
        }
        $.ajax({
            type: "POST",
            crossDomain: true,
            url: baseUrl + '' + url,
            contentType: 'application/json',
            data: JSON.stringify(dataTopost),
            headers: this.beforeSend(),
            success: function (result) {
                if (showprg == null || showprg == true) {
                    Common.RemoveModal();
                }
                callBack(true, result);
            },
            error: function (result) {
                if (showprg == null || showprg == true) {
                    Common.RemoveModal();
                }
                if (result.status == 0) {
                    callback(false, result.statusText);
                    Common.showError("No internet, please check your internet connection");
                    return;
                }
                try {
                    callBack(false, JSON.parse(result.responseText));
                } catch (e) {

                    callBack(false, "");
                }
                try {
                    var err = JSON.parse(result.responseText);
                    Common.showError(Common.escapeRegExp(JSON.stringify(err.errors)));
                } catch (e) {
                    Common.showError(result.statusText);
                }
            }
        });
    },
    Get: function (url, callback, showprg) {
        if (showprg == null || showprg == true) {
            Common.ShwoprogressModal();
        };
        $.ajax({
            type: "GET",
            crossDomain: true,
            url: baseUrl + '' + url,
            contentType: 'application/json',
            headers: this.beforeSend(),
            success: function (result) {
                if (showprg == null || showprg == true) {
                    Common.RemoveModal();
                };
                callback(true, result);
            },
            error: function (result) {
                console.log(result);

                if (showprg == null || showprg == true) {
                    Common.RemoveModal();
                }
                if (result.status == 0) {
                    callback(false, result.statusText);
                    Common.showError("No internet, please check your internet connection");
                    return;
                }
                try {
                    callback(false, JSON.parse(result.responseText));
                } catch (e) {
                    callback(false, result.responseText);
                }
                try {
                    var err = JSON.parse(result.responseText);
                    Common.showError(Common.escapeRegExp(JSON.stringify(err.errors)));
                } catch (e) {
                    Common.showError(result.statusText);
                }
            }
        });
    },
    LoadPage: function (url, callBack) {

        Common.ShwoprogressModal();
        $.ajax({
            type: "GET",
            url: url,
            success: function (result) {
                Common.RemoveModal();
                callBack(true, result);
            },
            error: function (result) {
                Common.RemoveModal();
                callBack(false, JSON.parse(result.responseText));
            }
        });
    },
    ShwoprogressModal: function () {
        try {
            var mDocument = document.getElementsByClassName('app')[0];
            document.getElementById('openModalProgress').style.width = mDocument.clientWidth + 'px';
            document.getElementById('openModalProgress').style.left = mDocument.offsetLeft + 'px';
            $('#openModalProgress').addClass('showModal');
        } catch (e) { }
    },
    RemoveModal: function () {
        $('#openModalProgress').removeClass('showModal');
    },
    SetJsonToLocal(key, Json) {
        localStorage.setItem(key, JSON.stringify(Json));
    },
    GetJsonFromLocal(key) {
        return JSON.parse(localStorage.getItem(key));
    },
    showError:function(err)
    {
        mAlert.Ini(mAlert.Type.ERROR, 200, 100, 'Error', err);
    },
    Device: { "available": true, "platform": "browser", "version": "9.0", "uuid": null, "cordova": "5.0.1", "model": "Safari", "manufacturer": "unknown", "isVirtual": false, "serial": "unknown" }
}