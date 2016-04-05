/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var minify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');
var concat = require('gulp-concat');



gulp.task('minifyjs', function () {
    gulp.src(['Scripts/underscore.min.js',
               'Scripts/jquery-1.10.2.js',
               'Scripts/angular.js',
               'Scripts/angular-resource.js',
               'Scripts/moment.js',
               'Scripts/angular-moment.js',
               'Scripts/angular-route.js'
    ])
        .pipe(sourcemaps.init())
        .pipe(concat('vender.js'))
        .pipe(minify())
        .pipe(sourcemaps.write())
      .pipe(gulp.dest('js'))
});


gulp.task('minifyjs_all', function () {
    gulp.src(['Scripts/Controllers/Meetup/index.js',
               'Scripts/Controllers/Meetup/create.js',
               'Scripts/Controllers/Mixins/tab.js',
               'Scripts/Controllers/Profile/profile-display.js',
               'Scripts/Directives/history-back.js',
               'Scripts/Directives/subpage-nav/subpage-nav.js',
               'Scripts/Directives/loading-circle/loading-circle.js',
               'Scripts/Router/router-config.js',
               'Scripts/jquery.placepicker.js',
               'Scripts/jquery.datetimepicker.full.js',
               'Scripts/ui-bootstrap-tpls-1.2.5.js',
               'Scripts/angular-confirm.js',
               'Scripts/Directives/enter-key.js',
               'Scripts/Directives/validation.js'
    ])
        .pipe(sourcemaps.init())
        .pipe(concat('all.js'))
        .pipe(minify())
        .pipe(sourcemaps.write())
      .pipe(gulp.dest('js'))
});

gulp.task('default', ['minifyjs', 'minifyjs_all']);