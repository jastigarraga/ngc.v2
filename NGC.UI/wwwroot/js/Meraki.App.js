angular.module("Meraki.UI",["ngAnimate","ngMaterial"])
angular.module("MerakiApp", ["Meraki.UI", "ngRoute"])
.config(function ($mdDateLocaleProvider) {
    $mdDateLocaleProvider.formatDate = function (date) {
        if (typeof date === "undefined") {
            return "";
        }
        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getFullYear();
        month = (month < 10 ? "0" + month : "" + month);
        return day + "/" + month + "/" + year;
    };
})
    .controller("MerakiMainController",["$scope","$mdSidenav","$mdDialog", function ($scope,$mdSidenav,$mdDialog) {
        $scope.openSidenav = function () {
            $mdSidenav("left").toggle();
        };
        $scope.showDialog = function (title, msg,aria) {
            $mdDialog.show(
                $mdDialog.alert()
                    .title(title)
                    .textContent(msg)
                    .ariaLabel(aria ||"error")
                    .ok('Aceptar')
            ); 
        };
    }])
    .controller("MerakiCustomerController", function ($scope) {
        function AddParams(Url, params) {
            Url += "?";
            var p = ""
            for (var key in params) {
                p += (p !== "" ? "&" : "") + key + "=" + params[key];
            }
            return Url + p;
        }
        $scope.read = function () {
            $scope.grid.read();
        };
        $scope.customers = [];
        $scope.filter = {};
        $scope.table = {
            pageSize: 5,
            pageSizes: [
                {
                    value: "5"
                }, {
                    value: "10"
                }, {
                    value: "20"
                }, {
                    value: "50"
                }
            ],
            page: 1,
            columns: [
                {
                    "name": "Name",
                    header: "Nombre",
                    templateUrl: AddParams(Templates.ViewBase, {
                        name: "Name",
                        model: "name"
                    }),
                    editTemplateUrl: AddParams(Templates.EditorBase, {
                        name: "Name",
                        model: "name"
                    })
                }, {
                    "name": "Surname1",
                    header: "Primer Apellido",
                    templateUrl: AddParams(Templates.ViewBase, {
                        name: "Surname1",
                        model: "surname1"
                    }),
                    editTemplateUrl: AddParams(Templates.EditorBase, {
                        name: "Surname1",
                        model: "surname1"
                    })
                }, {
                    "name": "Surname2",
                    header: "Segundo Apellido",
                    templateUrl: AddParams(Templates.ViewBase, {
                        name: "Surname2",
                        model: "surname2"
                    }),
                    editTemplateUrl: AddParams(Templates.EditorBase, {
                        name: "Surname2",
                        model: "surname2"
                    })
                }, {
                    "name": "Email",
                    header: "Email",
                    templateUrl: AddParams(Templates.ViewBase, {
                        name: "Email",
                        model: "email",
                        type: "email"
                    }),
                    editTemplateUrl: AddParams(Templates.EditorBase, {
                        name: "Email",
                        model: "email",
                        type: "email"
                    })
                }, {
                    "name": "Date",
                    header: "Fecha",
                    templateUrl: AddParams(Templates.ViewDate, {
                        model: "date"
                    }),
                    editTemplateUrl: AddParams(Templates.EditorDate, {
                        model: "date",
                    })
                }, {
                    "name": "LasSent",
                    header: "Envío",
                    templateUrl: AddParams(Templates.ViewDate, {
                        model: "lastSent"
                    }),
                    editTemplateUrl: AddParams(Templates.EditorDate, {
                        model:"lastSent"
                    })
                },
            ],
            transport: {
                read: {
                    url: ApiRoutes.Customers,
                    data: function (data) {
                        data.filter = JSON.stringify($scope.filter);
                        return data;
                    }
                },
                insert: {
                    url: ApiRoutes.Customers,
                    method:"PUT"
                },
                update: {
                    url: ApiRoutes.Customers,
                    method:"POST"
                },
                delete: {
                    url: ApiRoutes.Customers,
                    method:"DELETE"
                }
            }
        };
    });
