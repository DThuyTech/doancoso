//var TrandingSlider = new Swiper('.tranding-slider', {
//    effect: 'coverflow',
//    grabCursor: true,
//    centeredSlides: true,
//    loop: true,
//    spaceBetween: 30,
//    slidesPerView: '2',
//    autoplay: {
//        delay: 1500,
//        disableOnInteraction: false,
//    },
//    coverflowEffect: {
//        rotate: 0,
//        stretch: 0,
//        depth: 100,
//        modifier: 2.5,
//    },
//    pagination: {
//        el: '.swiper-pagination',
//        clickable: true,
//    },
//    navigation: {
//        nextEl: '.swiper-button-next',
//        prevEl: '.swiper-button-prev',
//    }
//});
var TrandingSlider = new Swiper(".tranding-slider", {
    slidesPerView: 1,
    spaceBetween: 20,
    loop: true,
    autoplay: {
        delay: 3000,
        disableOnInteraction: false,
    },
    speed: 2000,
    effect: "coverflow",
    coverflowEffect: {
        rotate: 3,
        stretch: 2,
        depth: 100,
        modifier: 5,
        slideShadows: false,
    },
    loopAdditionSlides: true,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
});




var team_slider = new Swiper(".team-slider", {
    slidesPerView: 3,
    spaceBetween: 30,
    loop: true,
    autoplay: {
        delay: 3000,
        disableOnInteraction: false,
    },
    speed: 2000,

    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    breakpoints: {
        0: {
            slidesPerView: 1.2,
        },
        768: {
            slidesPerView: 2,
        },
        992: {
            slidesPerView: 3,
        },
        1200: {
            slidesPerView: 3,
        },
    },
});