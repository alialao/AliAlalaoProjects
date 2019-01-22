<?php
session_start();
if(!isset( $_SESSION['id'])){
    header("location:Account.php");
}

ob_start();
    date_default_timezone_set('Europe/Amsterdam');
include 'dbconnect.php';
include 'comments.inc.php';
?>
<!doctype html>
<html>
	<head>
	    <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <!--First mobile meta-->
        <meta name="viewport" content="width=device-width, initial-scale=1">
		<title>Feedback</title>
		<link rel="stylesheet" href="css/bootstrap.css" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
        <link rel="stylesheet" href="css/style.css" />
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
        <!--
		    <script type="text/javascript" src="js/jQuery.js"></script>
		    <script type="text/javascript" src="js/script.js"></script>
        -->
	</head>
    <body>
       
        <div class="feedback-body">
        <!-- Start header and nav    -->
        <?php $pageName = 'feedback'; include 'includes/Header-Nav.php' ?>
        <!-- Start header and nav    -->
        <div class="wrapper">	    
            <div class="page-data">
                <h1>Share your experience!!</h1>
            </div>	    
            <div class="comment-wrapper"> 
            <h3 class="who-says"><span>Says:</span><?php echo $_SESSION['fname']." ".$_SESSION['lname']; ?></h3>

            <?php 
            echo "<form method='POST' action='".setComments($conn)."'>
               <input type='hidden' name ='email' value ='".$_SESSION['email']."'>
               <input type='hidden' name ='date' value ='".date('Y-m-d H:i:s')."'>
                <textarea name='message'></textarea><br>
                <button type='submit' name='commentSubmit'>Comment</button>
                
            </form>";

            getComments($conn);
            ?>
            </div>
        </div>
        
        
        <!-- Start footer1-2    -->
        <?php include 'includes/footer1-2.php' ?>
        <!-- End footer1-2    -->
        </div>
        <script src="js/bootstrap.min.js"></script>
        <script src="js/plugins.js"></script>
    </body>
</html>