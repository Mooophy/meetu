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
            },
            join: {
                get: function () {
                    return $resource('/api/Joins');
                }
            },
            meetup: {
                get: function () {
                    return $resource('/api/Meetups');
                },
                put: function () {
                    return $resource('/api/Meetups', null, {
                        'update': { method: 'PUT' }
                    });
                }
            }
        }
    }
})();