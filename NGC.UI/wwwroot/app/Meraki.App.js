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
        $scope.closeSidenav = function () {
            $mdSidenav("left").close();
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
            $mdDialog.show($mdDialog.prompt()
                .title('Probar configuración')
                .textContent('Introduzca una dirección de correo a la que enviar')
                .placeholder('Email')
                .ariaLabel('email')
                .ok('Probar')
                .cancel('Cancelar'))
                .then(cb);
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
    .controller("MerakiTemplateEditorController", ["$scope", "$mdDialog", "$http","$mdToast", function ($scope, $mdDialog, $http,$mdToast) {
        $scope.templates = [];
        function loadTemplates() {
            $scope.ready = false;
            $http({
                url: ApiRoutes.Templates
            }).then(function (response) {
                $scope.loading = false;
                $scope.templates = response.data;
                $scope.template = ($scope.templates && typeof $scope.templates.length !== "undefined" ? $scope.templates[0] : null);
                }, function (error) {
                    $scope.ready = true;
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
        $scope.save = function () {
            $scope.ready = false;
            $http({
                url: ApiRoutes.Templates,
                data: $scope.template,
                method: $scope.template.Id ? "POST" : "PUT"
            }).then(function (response) {
                $scope.templates[$scope.templates.indexOf(template)] = response.data;
                $scope.templates = $scope.data;
                $scope.ready = true;
                    $mdToast.show({
                        textContent: "Guardado!",
                        position: "top"
                    });
                }, error);
        }
        $scope.delete = function (template) {
            
            $mdDialog.show(
                $mdDialog.confirm()
                    .title("Confirmar eliminar")
                    .textContent("¿Seguro que desea eliminar la plantilla?")
                    .ok("Si")
                    .cancel("No")
            )
                .then(function () {
                    if (template.Id) {
                        $scope.ready = false;
                        $http({
                            url: ApiRoutes.Templates + "?id=" + template.Id,
                            method: "DELETE"
                        }).then(function (response) {
                            $scope.ready = true;
                            $scope.templates.splice($scope.templates.indexOf(template), 1);
                            $mdToast.show({
                                textContent: "Eliminado!",
                                position: "top"
                            });
                        }, error)
                    } else {
                        $scope.templates.splice($scope.templates.indexOf(template), 1);
                        $scope.template = {};
                        $mdToast.show({
                            textContent: "Eliminado!",
                            position: "top"
                        });
                    }
                });
        };
        function error(error) {
            $scope.ready = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .title("Error")
                    .textContent(error.data)
                    .ok("Aceptar")
            );
        }
        loadTemplates();
        $scope.ready = true;
    }]);
angular.module("Meraki.UI");