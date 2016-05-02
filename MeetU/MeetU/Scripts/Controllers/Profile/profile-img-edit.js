(function () {
    "use strict";
    angular
        .module('meetupModule')
        .controller('ProfileImageEditController', ProfileImageEditController);
    ProfileImageEditController.$inject = ["$scope", "$resource", "$location"];
    function ProfileImageEditController($scope, $resource, $location) {
        var vm = this;
        var User = $resource('/api/loggedUser');
        var ProFilePicture = $resource('api/ProfilePicture');

        User.query(function (user) {
            vm.loggedUser = user[0];
        });

        $scope.Image = '';
        $scope.CroppedImage = '';
        var handleFileSelect = function (event) {
            var file = event.currentTarget.files[0];
            var reader = new FileReader();
            reader.onload = function (event) {
                $scope.$apply(function ($scope) {
                    $scope.Image = event.target.result;
                });
            };
            reader.readAsDataURL(file);
        };
        angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
        vm.submit = function () {
            if ($scope.CroppedImage == 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIwAAACMCAYAAACuwEE+AAACW0lEQVR4Xu3SsQ0AAAzCsPL/070hu5mZIu9MgVBg4euqwAEDQSoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrlDAwDqQAwKZczMAykAsCkXM7AMJAKAJNyOQPDQCoATMrl/LT1AI3GBqJCAAAAAElFTkSuQmCC') {
                alert("Please select your image");
                return;
            }
            if ((/^data:image\/png;base64/.test($scope.CroppedImage) | /^data:image\/jpeg;base64/.test($scope.CroppedImage))==0) {
                alert("Image must be png/jpeg format");
                return;
            }
            var picture = new ProFilePicture({
                userId:
                    vm.loggedUser.userId, 
                data:
                    $scope.CroppedImage,
            });
            picture.$save();
            $location.path('/Profile/' + vm.loggedUser.userId);
        }
    }
})();