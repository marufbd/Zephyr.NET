!function ($) {

    $('form').on('submit', function() {
        if ($('form').valid())
            $('.btn[type="submit"]').button('loading');
    });    

} (window.jQuery);