
var app = angular.module("myApp", []);
app.controller("loginCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.errorLogin = "";
    $scope.login = function () {
        if ($scope.username && $scope.username.length > 0 && $scope.password && $scope.password.length > 0) {
            $http.post("/Home/Login", { username: $scope.username, password: $scope.password })
                .then(function (data) {
                    if (data.data === 1) {
                        console.log("Başarılı");
                        $scope.errorLogin = "Başarıyla giriş yaptınız.";
                        location.href = "/";
                    }
                    else {
                        $scope.errorLogin = data.data;
                        console.log("Başarısız");
                    }
                }, function (err) {
                    console.log(err);
                    $scope.errorLogin = "Kullanıcı adı ve şifrenizi girin.";
                });
        } else {
            $scope.errorLogin = "Kullanıcı adı ve şifrenizi girin.";
        }
    };
}]);
app.controller("registerCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.errorLogin = "";
    $scope.register = function () {
        if ($scope.username && $scope.username.length > 0 && $scope.password && $scope.password.length > 0
            && $scope.rePassword && $scope.rePassword.length > 0) {
            if ($scope.rePassword !== $scope.password) {
                $scope.errorLogin = "Şifreler uyuşmuyor.";
                return;
            }
            $http.post("/Home/Register", { username: $scope.username, password: $scope.password })
                .then(function (data) {
                    if (data && data.data) {
                        console.log("Başarılı");
                        $scope.errorLogin = "Başarıyla kayıt oldunuz yaptınız.";
                        location.href = "/Home/Login";
                    }
                    else {
                        $scope.errorLogin = data.data;
                        console.log("Başarısız");
                        $scope.errorLogin = "Kayıt başarısız oldu.";
                    }
                }, function (err) {
                    console.log(err);
                    $scope.errorLogin = "Kayıt başarısız oldu.";
                    });
        } else {
            $scope.errorLogin = "Kullanıcı adı ve şifrenizi girin.";
        }
    };
}]);

app.controller("createForm", function ($scope, $http) {
    $scope.Name = "";
    $scope.Description = "";
    $scope.Fields = [];
    $scope.pageError = "";

    $scope.addField = function () {
        if ($scope.FieldName && $scope.FieldName.length > 0
            && $scope.DataType && $scope.DataType.length > 0) {
            $scope.Fields.push({
                Name: $scope.FieldName,
                DataType: $scope.DataType,
                Required: $scope.Required
            });
        }
    };

    $scope.createNewForm = function () {
        $scope.pageError = "";
        if ($scope.Name.length === 0) {
            $scope.pageError = "Lütfen formunuza isim belirtin.";
            return;
        }
        if ($scope.Fields.length === 0) {
            $scope.pageError = "Lütfen formunuza alan(lar) ekleyin.";
            return;
        }
        var sendData = { Name: $scope.Name, Description: $scope.Description, Fields: $scope.Fields };
        
        $http.post("/Form/Add", JSON.stringify(sendData))
            .then(function (data) {
                console.log(data);
                location.reload();
            }, function (err) {
                $scope.pageError = "Bir hata oldu!";
                console.log(err);
            });
    };
});

app.controller("listFormsCtrl", function ($scope, $http) {
    $scope.forms = [];
    $http.post("/form/getForms")
        .then(function (response) {
            if (response.data !== null)
                $scope.forms = response.data;
        });
});

app.filter("mydate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        var m = x.match(re);
        if (m) return new Date(parseInt(m[1]));
        else return null;
    };
});