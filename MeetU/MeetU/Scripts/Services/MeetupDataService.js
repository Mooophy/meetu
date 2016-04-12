(function () {
    "use strict";
    angular
        .module('meetupModule')
        .factory('MeetupDataService', MeetupDataService)
     function MeetupDataService() {
         var temp = {};
         return {
             getTemp: function () {
                 return temp;
             },
             setTemp : function (value) {
                 temp = value;
         }
         }
     }   
})();