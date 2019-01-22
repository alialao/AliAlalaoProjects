<?php
session_start();
?>
<!DOCTYPE HTML>
<html>
	<head>
	    <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <!--First mobile meta-->
        <meta name="viewport" content="width=device-width, initial-scale=1">
		<title><?php if (isset($pageName)){ echo $pageName;} ?></title>
        <link rel="icon" href="img/fav1.ico" type="image/gif" sizes="16x16">
		<link rel="stylesheet" href="css/bootstrap.css?t=<?php echo time(); ?>" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css?t=<?php echo time(); ?>" />
        <link rel="stylesheet" href="css/style.css" />
        <script src="//ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js?t=<?php echo time(); ?>"></script>
        <script>
		$(document).ready(function(){
			$('#ex1').zoom();
			$('#ex2').zoom();
			$('#ex3').zoom();

		});
	    </script>
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
            <header class="hidden-xs ">
                <div class="img-holder"></div>
            </header>
            <!--start our navbar-->
            <nav id="nav_bar" class="navbar navbar-default  ">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#ournavbar" aria-expanded="false">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="index.php">BESTIVAL</a>
                        <!-- <a class="navbar-brand" href="index.html">BESTIVAL</a>-->
                    </div>
                    <div class="collapse navbar-collapse" id="ournavbar">
                        <ul class="nav navbar-nav navbar-left upper">
                            <li <?php if($pageName == 'BESTIVAL'){echo "class = 'active'";} ?>><a href="index.php">HOME</a></li>
                            <li <?php if($pageName == 'News'){echo "class = 'active'";} ?>><a href="News.php">News</a></li>
                            <li <?php if($pageName == 'Areas'){echo "class = 'active'";} ?>><a href="areas.php">Areas</a></li>
                            <li <?php if($pageName == 'Tickets'){echo "class = 'active'";} ?>><a href="Ticket.php">Tickets</a></li>
                            <li <?php if($pageName == 'feedback'){echo "class = 'active'";} ?>><a href="feedback.php">Feedback</a></li>
                            <li <?php if($pageName == 'Contact'){echo "class = 'active'";} ?>><a href="Contact.php">Contact</a></li> 
			    <?php if(!isset($_SESSION['email'])){  
                            echo "<li "; ?> <?php if($pageName == 'Login'){echo "class = 'active'";} ?> <?php echo "><a href='Account.php'>Login</a></li>"; } ?>
			    <?php
				if(isset($_SESSION['fname'])){
				echo "<li ";?><?php if($pageName == 'Balance'){echo "class = ' active'";} ?> <?php echo"><a href='Balance.php'><span class='main-color'>".$_SESSION['fname']."</span></a></li>
                                <li ><a href='logout.php'><span class='logout-style'>Logout</span></a></li>";
				}
			    ?>
                          

			    
                        </ul>
                    </div>
                </div> 
            </nav>
            <!-- end our navbar--> 