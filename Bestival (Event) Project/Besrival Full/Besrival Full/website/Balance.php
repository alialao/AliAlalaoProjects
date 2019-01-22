<?php
include 'dbconnect.php';
include 'comments.inc.php';
session_start();
if(!isset( $_SESSION['id'])){
    header("location:Account.php");
}

if(isset($_POST['balanceSubmit'])){
	$email=$_SESSION['email'];
	$a = $_POST['newAmount'];
    $a = preg_replace('/\D/', '',$a);
    setcookie("balance", "true", time() + 120);
    setcookie("email", $email, time() + 120);
    setcookie("a", $a, time() + 120);
    
    header("location:payment.php");


        }

        $email= $_SESSION['email'];
       
        $sql2 = "select * from customers where email_address='$email'";
        $result2 = $conn->query($sql2);

        if ($result2->num_rows > 0) {

        $row = $result2->fetch_assoc();
        $_SESSION['newBalance']= $row['deposit_money'];

        }



?>


<!-- Start header and nav    -->
<?php $pageName = 'Balance'; include 'includes/Header-Nav.php' ?>
<!-- Start header and nav    -->
<!-- Start Content -->
<section class="balance ">
    <div class="container"> 
        <div class="row">
            <div class=" box col-lg-4 col-md-6 col-sm-6 col-xs-12">
                <div class="user-details text-center">
                    <h2>Account Details</h2>
                    <p class="user-name"><?php echo $_SESSION['fname']." ". $_SESSION['lname']; ?></p>
                    <p class="user-email"><?php echo $_SESSION['email'];  ?></p>
                    <p class="user-dob"><?php echo $_SESSION['dob'];  ?></p>
                    <p class="user-phone"><?php echo $_SESSION['phone'];  ?></p>
                    <a href="editUserDetails.php">Edit</a>
                </div>
            </div>
            <div class=" box col-lg-8 col-md-6 col-sm-6 col-xs-12 ">
                <div class="balance-details text-center">
                    <h2 class="text-center">Your account balance is: $<?php echo  $_SESSION['newBalance']; ?> </h2> 
                    <p> Top-Up your deposit here:</p>
                    <form class='form-balance text-center' method='post' action="<?php echo $_SERVER['PHP_SELF']?>">
                        <input type='text' name='newAmount'/>
                        <input class='bt-Add' type='submit' name='balanceSubmit'  value='Add'/>
                       
                    </form>
                    
                </div>
            </div>
        </div>
    </div>
</section>
<!-- Start footer1-2    -->
<?php include 'includes/footer1-2.php' ?>
<!-- End footer1-2    -->
