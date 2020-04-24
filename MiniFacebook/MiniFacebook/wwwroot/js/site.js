// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $(window).scroll(function () {

        if ($(this).scrollTop() > 300) {
            $('.scroll-up').fadeIn();
        } else {
            $('.scroll-up').fadeOut();
        }
    });

    $('.scroll-up').click(function () {
        $("html,body").animate({
            scrollTop: 0
        }, 1000);
        return false;
    });
});

$(function () {
    $("button[value='Google']>i").addClass('fab fa-google google-icon');
    $("button[value='Google']").addClass('google-btn');
    $("button[value='Facebook']>i").addClass('fab fa-facebook-f face-icon');
    $("button[value='Facebook']").addClass(' face-btn');

});