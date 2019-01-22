<?php
// Check if user coming from a request
if($_SERVER['REQUEST_METHOD'] == 'POST'){
    // Assign variables
    $user = filter_var($_POST['username'],FILTER_SANITIZE_STRING);
    $email =filter_var($_POST['email'],FILTER_SANITIZE_STRING);
    $message =filter_var($_POST['message'],FILTER_SANITIZE_STRING);
    // creating array of errors
    $formErrors = array();
    if (strlen($user) < 3){
        $formErrors[] ='Username must be larger then <strong>3</strong> characters!';
    }
    if (strlen($email) < 1){
    $formErrors[] ='Email can\'t be empty!';
    }
    if (strlen($message) < 40){
        $formErrors[] ='Message can\'t be less than <strong>40</strong> characters!';
    }
    //if no errors send the email
    $headers = 'From ' .$user .' Email ' .$email .'\r\n';
    if(empty($formErrors)){   
        mail('boooood.66@gmail.com','Contact form',$message,$headers);
        $user ='';
        $email='';
        $message='';
        $success ='<div class="alert alert-success">We have received your message</div>';
    }
}
?>
<!-- Start header and nav    -->
<?php $pageName = 'Contact'; include 'includes/Header-Nav.php' ?>
<!-- Start header and nav    -->

<!--start contact-->
<section class="contact">
    <div class="container">
        <div class="row">
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div class="info">
                    <h2><a name="contact_me"> Contact Us </a></h2>
                    <p>We try to answer all your questions here on the official Bestival website or Whatsapp: +31682676772.</p>
                </div>
            </div>
            <div class="col-lg-6">
                <form class="form contact-form" method="POST" action="<?php echo $_SERVER['PHP_SELF']?>">
                    <?php if(! empty($formErrors)){ ?>
                    <div class="alert alert-danger alert-dismissable" role="start">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">Close</span></button> 
                    <?php
                        foreach ($formErrors as $error){
                        echo $error . '<br>';
                        }
                    ?>
                    </div>
                    <?php } ?>
                    <?php 
                        if(isset($success)) {echo $success ;}
                    ?>
                    <div class="form-group">
                        <input class="username" type="text" name="username" placeholder="Name" value="<?php  if (isset($user)){echo $user;}?>"/>
                        <div class="alert alert-danger custom-alert ">
                            Username must be larger then <strong>3</strong> characters!
                        </div>
                    </div>
                    <div class="form-group">
                        <input class="email" type="email" name="email" placeholder="Email" value="<?php  if (isset($email)){echo $email;} ?>" />
                        <div class="alert alert-danger custom-alert">
                            Email can't be empty!
                        </div>
                    </div>
                    <div class="form-group">
                        <textarea class="message"  name="message" placeholder="Your Message" > <?php  if (isset($message)){echo $message;}?></textarea>
                        <div class="alert alert-danger custom-alert">
                            Message can't be less than <strong>40</strong> characters!    
                        </div>
                    </div>
                    <input type="submit" value="Send"/>
                </form>
            </div>
        </div>
    </div>
</section>
<!--End contact-->
<!-- Start footer1-2    -->
<?php include 'includes/footer1-2.php' ?>
<!-- End footer1-2    -->
