(function () {
    "use strict";
    angular
        .module('meetupModule')
        .factory('$api', $api);
    $api.$inject = ['$resource'];
    function $api($resource) {
        return {
            comment: {
                get: function () {
                   return $resource('/api/Comments/');
                }
            }
        }
    }
})();