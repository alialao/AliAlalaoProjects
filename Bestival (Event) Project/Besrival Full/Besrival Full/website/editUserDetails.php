<?php
include 'dbconnect.php';
include 'comments.inc.php';
session_start();
if(!isset( $_SESSION['id'])){
    header("location:Account.php");
}

if(isset($_POST['detailsUpdate'])){
    $fname = $_POST['fname'];
    $lname = $_POST['lname'];
    $email = $_POST['email'];
    $dob = $_POST['dob'];
    $phone = $_POST['phone'];
    $oldEmail= $_SESSION['email'];
    
    $sql= "UPDATE customers
           SET first_name = '$fname', last_name = '$lname', dof = '$dob', email_address ='$email', phone_number = '$phone'
           WHERE email_address = '$oldEmail'";
    
    $result = mysqli_query($conn, $sql);
    $count = mysqli_num_rows($result);
    $row = mysqli_fetch_assoc($result);
  header("location:logout.php");

    
    
}



?>
<!-- Start header and nav    -->
<?php $pageName = 'Account'; include 'includes/Header-Nav.php' ?>
<!-- Start header and nav    -->
<section class="ticket ">
    <div class="container">
        <div class="row">
            <div class=" box col-lg-12 col-md-12 col-sm-12 col-xs-12  ">
                <div id="first-form" style="margin: 40px auto 40px auto;">
                    <div class="individual-box">                 
                        <h2 class="text-center">Change Your Details</h2> 
                        <form class="form-ticket" method='post' action='<?php echo $_SERVER['PHP_SELF']?>'>
                            <div class="row1">

                                <label> First name *</label>
                                <input type="text" name='fname' />
                            </div>
                            <div class="row1">
                                <label> Last name * </label>
                                <input type="text" name= 'lname'/>
                            </div>
                            <div class="row1">
                                <label> Email address *	</label>
                                <input type="text" name='email'/>
                            </div>
                            <div class="row1">
                                <label> Date Of Birth *	</label>
                                <input type="date" name="dob" placeholder="mm/dd/yyyy">
                            </div>
                            <div class="row1">
                                <label> Phone</label>
                                <input type="text" name="phone" />
                                
                            </div>
                                <div class="row1 text-center">
                                <input type="submit" value="Update" name='detailsUpdate'/>
                            </div>

                        </form>
                    </div>
                </div>
            </div>     
        </div>
    </div>
</section>
<!-- Start footer1-2    -->
<?php include 'includes/footer1-2.php' ?>
<!-- End footer1-2    -->

