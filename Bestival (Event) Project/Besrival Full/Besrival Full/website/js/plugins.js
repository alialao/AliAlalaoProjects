/*global $, alert, console*/
$(function () {
    'use strict';
    $(window).scroll(function () {
        if ($(window).scrollTop() > 199) {
        $('.navbar').css({
        'position' : 'fixed',
        'top' : '0',
        'width' : '100%',
        'background' : '#000',
        'z-index' : '100'
            
        }); 
            
        $('body').css({'margin-top': '50px'});
        }   
        if ($(window).scrollTop() < 200) {
            $('.navbar').removeAttr('style');
            $('body').css({'margin-top': '0'});

        }
    });
    //   Trigger Nice Scroll Plugin
    $('html').niceScroll({
        
        cursorcolor: '#167ac6',
        cursorwidth: 10,
        cursorborderradius: 5,
        cursorborder: '1px solid #167ac6'  
    });
    //show indivisual or group form depinding on what user choosed
        $("#click-first-form").click(function () {
        $("#first-form").slideToggle();
        $("#second-form").slideUp();
        $("#third-form").slideUp();
        if($("#camping1").is(':checked')){
            document.getElementById('price').innerHTML="$80";
        }else{
            document.getElementById('price').innerHTML="$55";

        }
    });
        $("#click-second-form").click(function () {
        $("#first-form").slideUp();
        $("#second-form").slideToggle();
        $("#third-form").slideUp();
        if($("#camping").is(':checked')){
            document.getElementById('price').innerHTML="$80";
        }else{
            document.getElementById('price').innerHTML="$55";
        }
        });
        $(".bt-camp").click(function(){
        $("#first-form").slideUp();
        $("#second-form").slideUp();
        $("#third-form").slideToggle();
        document.getElementById('price').innerHTML="$20";
    });
    //hide buy later button if the user select camping spot
    $("#camping").click(function(){
        if($("#camping").is(':checked')){
            $("#buylater").hide();
            $(".select-camping").show();
            document.getElementById('price').innerHTML = "$80";

        }
        else{
            $("#buylater").show();
            $(".select-camping").hide();
            document.getElementById('price').innerHTML="$55";
        }
    });
    $("#camping1").click(function(){
        if($("#camping1").is(':checked')){
            $("#buylater1").hide();
            $(".select-camping1").show();
            document.getElementById('price').innerHTML="$80";

        }else{
            $("#buylater1").show();
            $(".select-camping1").hide();
            document.getElementById('price').innerHTML="$55";

        }
    });   
    
    //start contact page code
    var userErrors = true;
    var emailErrors = true;
    var messageErrors = true;
    //show alert if the name is less than 4 letters
    $('.username').blur(function () {
        if ($(this).val().length < 4) { // show error  
            $(this).css('border','1px solid #a94442').parent().find('.custom-alert').fadeIn(300);
            userErrors = true;
        }
        else{// no errors
            $(this).css('border','1px solid #080').parent().find('.custom-alert').fadeOut(300);
            userErrors =false;
        }
    });
    //show alert if there is no letters enterd
    $('.email').blur(function () {
        if ($(this).val() === '') {
            $(this).css('border','1px solid #a94442').parent().find('.custom-alert').fadeIn(300);
            emailErrors = true;
        }
        else{
            $(this).css('border','1px solid #080').parent().find('.custom-alert').fadeOut(300);
            emailErrors =false;
        }
    });
    //show alert if the letters of the message is less than 40
        $('.message').blur(function () {
        if ($(this).val().length < 40) {   
             $(this).css('border','1px solid #a94442').parent().find('.custom-alert').fadeIn(300);
            messageErrors = true;
        }
        else{
            $(this).css('border','1px solid #080').parent().find('.custom-alert').fadeOut(300).end; 
            messageErrors =false;
        }
    });
    //submit form validation
    $('.contact-form').submit(function(e){
        if(userErrors === true || emailErrors === true || messageErrors === true  ){
            e.preventDefault();
            $('.username, .email, .message').blur();
        }else{}
         });
    //End contact page code

         

	 
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
});
