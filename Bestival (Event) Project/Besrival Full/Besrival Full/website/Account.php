<?php
include 'dbconnect.php';
include 'comments.inc.php';
session_start();
?>


<!-- Start header and nav    -->
<?php $pageName = 'Login'; include 'includes/Header-Nav.php' ?>
<!-- Start header and nav    -->
<div class="form-wrapper2">
    <h1> Login</h1>
    <div class="form-group ">
        <div class="boxCode">
            <div class="smth">
              <?php
               echo "<form method ='POST' action='".getLogin($conn)."'>
                <input type='text' name='email' placeholder='Email address'>
                <input type='password' name='password' placeholder='password' autocomplete='off'>
                
            </div>
        </div>
        <p>forgot your password? Click <a href='resetPassword.php'>Here</a></p>
        <a href= 'Balance.php'><button type='submit' class='btnCheck' name='loginSubmit' >Login</button></a>
        </form>";
    
                ?>
    </div>
</div>
<!-- Start footer1-2    -->
<?php include 'includes/footer1-2.php' ?>
<!-- End footer1-2    -->
