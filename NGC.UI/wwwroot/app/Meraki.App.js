function to_error_string(error) {
    return typeof error !== "undefined" ? (error.message || error.statusText || error) : "Error desconocido";
}
angular.module("Meraki.UI", ["ngAnimate", "ngMaterial"])
    .service('authInterceptor', ["$q","$rootScope", function ($q,$rootScope) {
        var service = this;

        service.responseError = function (response) {
            if (response.status == 401) {
                window.location = Routes.Login;
            } else if (response.status == 404) {
                $rootScope.$broadcast("meraki-alert", {
                    title: "Error 404!!",
                    message:"No se ha podido encontrar la dirección " + response.config.url
                });
            }
            return $q.reject(response);
        };
    }])
    .config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    }])
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
    .controller("MerakiMainController", ["$scope", "$mdSidenav", "$mdDialog", function ($scope, $mdSidenav, $mdDialog) {
        $scope.$on("meraki-alert", function (evt,data) {
            $mdDialog.show(
                $mdDialog.alert()
                    .title(data.title)
                    .textContent(data.message)
                    .ariaLabel(data.aria || "error")
                    .ok("Aceptar")
            );
        });
        $scope.openSidenav = function () {
            $mdSidenav("left").toggle();
        };
        $scope.loginFormSubmit = function (e) {
            if (!$scope.loginForm.$valid) {
                e.preventDefault();
                e.stopPropagation();
                $scope.loginForm.$setSubmitted(true);
            }
        };
        $scope.showDialog = function (title, msg, aria) {
            $mdDialog.show(
                $mdDialog.alert()
                    .title(title)
                    .textContent(msg)
                    .ariaLabel(aria || "error")
                    .ok('Aceptar')
            );
        };
    }])
    .controller("MerakiCustomerController", function ($scope) {
        $scope.grid = {
            cols: [
                {
                    title: "Email",
                    name: "Email",
                    width: "120px",
                    templateUrl: Templates.ViewBase
                }, {
                    title: "Fecha",
                    name: "Date",
                    templateUrl: Templates.ViewDate
                }, {
                    title: "Envío",
                    name: "LastSent",
                    templateUrl: Templates.ViewDate
                },
                {
                    title: "Nombre",
                    name: "Name",
                    templateUrl: Templates.ViewBase + "?model=Name"
                }, {
                    title: "Primer apellido",
                    name: "Surname1",
                    templateUrl: Templates.ViewBase
                }, {
                    title: "Segundo apellido",
                    name: "Surname2",
                    templateUrl: Templates.ViewBase
                }
            ],
            templateUrl: template("MerakiCustomerEditorTemplate"),
            transport: {
                read: {
                    url: ApiRoutes.Customers
                },
                update: {
                    url: ApiRoutes.Customers,
                    method:"POST"
                },
                insert: {
                    url: ApiRoutes.Customers,
                    method:"PUT"
                },
                delete: {
                    url: ApiRoutes.Customers,
                    method: "DELETE"
                }
            },
            pageSize:5,
            pageSizes:[5,10,20,50]
        }

    })
    .controller("merakiConfigController", ["$scope", "$http", "$mdDialog", function ($scope, $http, $mdDialog) {
        function askForEmail(cb) {
            $mdDialog.show({
                template: "<md-dialog aria-label='Introduzca email'>"
                + "  <md-dialog-header>"
                + "    <p>Introduzca la dirección de destino</p>"
                + "  </md-dialog-header>"
                + "  <md-dialog-content>"
                + "    <form name='from'>"
                + "      <md-input-container>"
                + "        <label>Destino</label>"
                + "        <input type='email' name='email' ng-model='to'/>"
                + "      </md-input-container>"
                + "    </form>"
                + "  </md-dialog-content>"
                + "  <md-dialog-actions>"
                + "    <md-button ng-click='close()'>Cancelar</md-button>"
                + "    <md-button ng-click='accept()'>Aceptar</md-button>",
                controller: function ($scope, $mdDialog) {
                    $scope.close = function () {
                        $mdDialog.hide();
                    };
                    $scope.accept = function () {
                        $mdDialog.hide();
                        cb($scope.to);
                    };
                }
            });
        }
        $scope.testEmailConfig = function () {
            askForEmail(function (to) {
                var config = angular.copy($scope.Email);
                config.To = to;
                $http({
                    url: ApiRoutes.EmailConfig,
                    method: "POST",
                    data: config
                })
                    .then(function (response) {
                        $scope.loading = false;
                    }, function (error) {
                        $scope.loading = false;
                    });
            });
        }
        $scope.saveEmailConfig = function () {
            if ($scope.emailForm.$valid) {
                $scope.loading = true;
                $http({
                    url: ApiRoutes.EmailConfig,
                    method: "PUT",
                    data: $scope.Email
                })
                    .then(function (response) {
                        $scope.loading = false;
                    },
                    function (error) {
                        $scope.loading = false;
                    })
            } else {
                $scope.emailForm.$setSubmitted(true);
            }
        }
        $scope.loadMailConfig = function () {
            $scope.loading = true;
            $http({
                url: ApiRoutes.EmailConfig
            })
                .then(function (response) {
                    $scope.Email = response.data;
                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                })
        };
        $scope.loadMailConfig();
    }])
    .controller("MerakiTemplateEditorController", ["$scope", "$mdDialog", "$http", function ($scope, $mdDialog, $http) {
        $scope.templates = [];
        function loadTemplates() {
            $scope.loading = true;
            $http({
                url: ApiRoutes.Templates
            }).then(function (response) {
                debugger;
                $scope.loading = false;
                $scope.templates = response.data;
                $scope.template = ($scope.templates && typeof $scope.templates.length !== "undefined" ? $scope.templates[0] : null);
            }, function (error) {
                $scope.loading = false;
                var alert = $mdDialog.alert({
                    title: "Error",
                    textContent: to_error_string(error),
                    ok: "Aceptar"
                });
                $mdDialog.show(alert).finally(function () {
                    alert = undefined;
                });
            });
        }
        $scope.new = function () {
            $scope.template = { Name: "Plantilla nueva" };
            $scope.templates.push($scope.template);
        }
        $scope.select = function (template) {
            $scope.template = template;
        }
        loadTemplates();
    }]);
angular.module("Meraki.UI");