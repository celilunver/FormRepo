
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
                IsActive: true
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

        $http.post("/Forms/CreateForm", { Name: $scope.Name, Description: $scope.Description, Fields: $scope.Fields, IsActive: true })
            .then(function (data) {
                console.log(data);
                location.reload();
            }, function (err) {
                $scope.pageError = "Bir hata oldu!";
                console.log(err);
            });
    };
});