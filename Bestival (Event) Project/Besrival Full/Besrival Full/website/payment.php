<?php
include 'dbconnect.php';
include 'comments.inc.php';
session_start();

if(isset($_POST['pay'])){
    if(isset($_COOKIE["individual"])){
    $fname=$_COOKIE["fname"];
    $lname=$_COOKIE["lname"];
    $email=$_COOKIE["email"];
    $dob=$_COOKIE["dob"];
    $phone=$_COOKIE["phone"];
    $password=$_COOKIE["password"];
    $reservationNumber= $_COOKIE["reservation"];
    $camp_l= $_COOKIE["camping_category"];
    $camp_n= $_COOKIE["camping_number"];
    
    $sql = "insert into customers (first_name, last_name, email_address, dof, password, phone_number, reservation_number, Camping_camping_category, Camping_camping_number)
                values('$fname', '$lname', '$email', '$dob', '$password', '$phone', '$reservationNumber' , '$camp_l', '$camp_n')";

        
                if(mysqli_query($conn, $sql)){
            $successMsg = 'smth.';
        }else{
            echo 'Error '.mysqli_error($conn);
        }
    setcookie("fname", $fname, time() - 120);
    setcookie("lname", $lname, time() - 120);
    setcookie("email", $email, time() - 120);
    setcookie("dob", $dob, time() - 120);
    setcookie("phone", $phone, time() - 120);
    setcookie("password", $password, time() - 120);
    setcookie("reservation", $reservationNumber, time() - 120);
    setcookie("camp_l", $camp_l, time() - 120);
    setcookie("camp_n", $camp_n, time() - 120);
    setcookie("individual", "true", time() - 120);
    header('location:Balance.php');


         
    }
    if(isset($_COOKIE["balance"])){
        
        $email=$_COOKIE["email"];
        $a=$_COOKIE["a"];
        $sql = "Update customers SET deposit_money =  deposit_money + '$a' where email_address = '$email' ";
	   

        
        if(mysqli_query($conn, $sql)){
            $successMsg = 'smth.';
        }else{
            echo 'Error '.mysqli_error($conn);
        }
        setcookie("balance", "true", time() - 120);
        setcookie("email", $email, time() - 120);
        setcookie("a", $a, time() - 120);
        header('location:Balance.php');
        
    }

}


?>


<!doctype html>

<html>
	<head>
		<title>Paypal</title>
		<meta charset="UTF-8">
		<meta name="viewport" content="initial-scale=1.0">
        <link href="img/paypal.ico" rel="icon" type="image/x-icon" />
        <link rel="stylesheet" href="css/payment-layout.css?t=<?php echo time(); ?>" />
        <script src="//ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js?t=<?php echo time(); ?>"></script>

	</head>
	<body>
        <div class="container1">
            <div class="logo1">Bestival</div>
            <div class="summarybox">
                <h3>your order summary</h3>
                <div class="description-title">
                    <label class="description">Descriptions</label>
                    <label class="amount">Amount</label>
                </div>
                <div class="content1">
                  
                   <div class="<?php if(isset($_POST['camp-buy-now'])){echo "hiddenn";} ?>">
                    <label class="item"><?php if(isset($_COOKIE["individual"])){echo "individual ticket";}elseif(isset($_COOKIE["balance"])){echo "Deposit amount";}else{echo "Group ticket";}?> </label>
                    <label class="item-quantity"><?php if(!isset($_COOKIE["balance"])){echo "x1";}else{echo " ";} ?></label>
                    <label class="item-amount">$<?php if(isset($_COOKIE["balance"])){echo $_COOKIE["a"];}else{echo "55";} ?></label>
                    </div>
                    <br>
                    <div  class="1">
                    <?php if(!isset($_COOKIE["balance"])){echo "
                    <label class='item'>camping </label>
                    <label class='item-quantity'> x1</label>
                    ";} ?>
                    
                    <label class="item-amount"><?php if (isset($_POST['camp']) || isset($_POST['camp2']) ) { echo "$20";}
                                                      elseif (isset($_POST['camp-buy-now'])){ echo "$20";}
                                                        elseif(isset($_COOKIE["balance"])){echo " ";}
                                                        else{echo "$0";}
                        ?></label>
                    </div>
                </div>
                <hr>
                <div class="total">
                    <label class="item-total">Items total</label>
                    <label class="item-amount">$<?php if (isset($_POST['camp1']) || isset($_POST['camp2'])) { echo "75";}
                                                      elseif(isset($_COOKIE["balance"])){echo $_COOKIE["a"];}

                                                          else{echo "55";}?></label>
                </div>
            </div>
            <form  method='post' action="<?php echo $_SERVER['PHP_SELF']?>">
            <input class="button" type="submit" name='pay' value="Pay Now">
            </form>
        </div>
        <script src="js/jquery-3.2.1.min.js?t=<?php echo time(); ?>"></script>
        <script src="js/jquery.nicescroll.min.js?t=<?php echo time(); ?>"></script>
        <script src="js/plugins.js?t=<?php echo time(); ?>"></script>
	</body>
</html>