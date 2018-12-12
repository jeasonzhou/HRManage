var CustomScript = function () {

    var handleSidebarMenuActiveLink = function (url, el, $state) {
        var menu = $('.page-sidebar-menu');
        menu.find('li > a').each(function () {
            var path = $(this).attr('href');
            if (path) {
                // url match condition
                path = path.toLowerCase();
                if (path.length > 1 && url == path) {
                    el = $(this);
                    return;
                }
            }
        });


        if (!el || el.size() == 0) {
            return;
        }

        if (el.attr('href') == 'javascript:;' ||
            el.attr('ui-sref') == 'javascript:;' ||
            el.attr('href') == '#' ||
            el.attr('ui-sref') == '#'
        ) {
            return;
        }

        var slideSpeed = parseInt(menu.data('slide-speed'));
        var keepExpand = menu.data('keep-expanded');

        // begin: handle active state
        if (menu.hasClass('page-sidebar-menu-hover-submenu') === false) {
            menu.find('li.nav-item.open').each(function () {
                var match = false;
                $(this).find('li').each(function () {
                    if ($(this).find(' > a').attr('href') === el.attr('href')) {
                        match = true;
                        return;
                    }
                });

                if (match === true) {
                    return;
                }

                $(this).removeClass('open');
                $(this).find('> a > .arrow.open').removeClass('open');
                $(this).find('> .sub-menu').slideUp();
            });
        } else {
            menu.find('li.open').removeClass('open');
        }


        menu.find('li.active').removeClass('active');
        menu.find('li > a > .selected').remove();
        // end: handle active state

        el.parents('li').each(function () {
            $(this).addClass('active');
            $(this).find('> a > span.arrow').addClass('open');

            if ($(this).parent('ul.page-sidebar-menu').size() === 1) {
                $(this).find('> a').append('<span class="selected"></span>');
            }

            if ($(this).children('ul.sub-menu').size() === 1) {
                $(this).addClass('open');
            }
        });
    };

    var handleTopMenuAjaxLoad = function () {
        $('.top-menu').on('click', '.ajaxify', function (e) {
            e.preventDefault();
            App.scrollTop();

            var url = $(this).attr("href");

            if (App.getViewPort().width < App.getResponsiveBreakpoint('md') && $('.page-sidebar').hasClass("in")) { // close the menu on mobile view while laoding a page 
                $('.page-header .responsive-toggler').click();
            }

            var sidebarMenuLink;
            $('.page-sidebar-menu  li > a.ajaxify').each(function () {
                var menu_url = $(this).attr("href");
                if (url == menu_url) {
                    sidebarMenuLink = $(this);
                    return;
                }

            });
            if (sidebarMenuLink != undefined && sidebarMenuLink.size() > 0) {
                $.ajax({
                    type: "GET",
                    cache: false,
                    url: url,
                    dataType: "html",
                    success: function (res) {
                        App.stopPageLoading();
                        var pageContent = $('.page-content');
                        var menu = $('.page-sidebar-menu');
                        var el;
                        menu.find('li > a').each(function () {
                            var path = $(this).attr('href');
                            if (path) {
                                // url match condition
                                path = path.toLowerCase();
                                if (path.length > 1 && url.toLowerCase() == path) {
                                    el = $(this);
                                    return;
                                }
                            }
                        });
                        if (el != null && el != undefined) {

                            el.parents('li').each(function () {
                                $(this).addClass('active');
                                $(this).find('> a > span.arrow').addClass('open');

                                if ($(this).parent('ul.page-sidebar-menu').size() === 1) {
                                    $(this).find('> a').append('<span class="selected"></span>');
                                }
                                $(this).children('ul.sub-menu').css('display', 'block');
                                if ($(this).children('ul.sub-menu').size() === 1) {
                                    $(this).addClass('open');
                                }
                            });

                        }
                        pageContent.html(res);
                        Layout.fixContentHeight(); // fix content height
                        App.initAjax(); // initialize core stuff
                    },
                    error: function (res, ajaxOptions, thrownError) {
                        App.stopPageLoading();
                        console.log(res);
                        pageContent.html('<h4>Could not load the requested content.</h4>');

                        for (var i = 0; i < ajaxContentErrorCallbacks.length; i++) {
                            ajaxContentSuccessCallbacks[i].call(res);
                        }
                    }
                });

                //Layout.loadAjaxContent(url, sidebarMenuLink);
                //CustomScript.setSidebarMenuActiveLink(url.toLowerCase(), null, null);
            } else {
                Layout.loadAjaxContent(url);
            }

        });
    };

    return {

        init: function () {
            var url = location.pathname.toLowerCase();
            handleSidebarMenuActiveLink(url, null, null);
            handleTopMenuAjaxLoad();
        },
        setSidebarMenuActiveLink: function (url, el, $state) {
            handleSidebarMenuActiveLink(url, null, null);
        }
    }
}();