<?php

function setComments($conn) {
    if(isset($_POST['commentSubmit'])){
        $email= $_POST['email'];
       
        $sql2 = "select * from customers where email_address='$email'";
        $result2 = $conn->query($sql2);

        if ($result2->num_rows > 0) {

        $row = $result2->fetch_assoc();
        $uid= $row['first_name']." ".$row['last_name'];

        }
            
        
        $date = $_POST['date'];
        $message = $_POST['message'];
        $sql = "insert into comments (customer_id, date, message) values ('$uid', '$date', '$message')";
        $result = $conn->query($sql);
        
    }
    
}

function getComments($conn){
    $sql = "select * from comments";
    $result = $conn->query($sql);
    while($row = $result->fetch_assoc()){
        echo "<div class='comment_box'><p>";
            echo "<div class='username'>".$row['customer_id']."</div>";
            echo $row['date'];
            echo "<hr>";
            echo nl2br($row['message']);
            echo "</p>";
	    if($_SESSION['id'] == 1){
	    echo "<form class='delete-form' method='POST' action = '".deleteComments($conn)."'>
            <input type='hidden' name ='comment_id' value= '".$row['comment_id']."'>
            <button type='submit' name ='commentDelete'>X</button>
            </form>";
		}
            
            echo "</div>";
            
    }
    
    

}

function deleteComments($conn){
    if (isset($_POST['commentDelete'])){
        $comment_id = $_POST['comment_id'];

        
        $sql ="DELETE FROM comments WHERE comment_id ='$comment_id'";
        $result =$conn->query($sql);
        header("location: feedback.php");
             exit();
    }
    
}

function getLogin($conn){
    if(isset($_POST['loginSubmit'])){
    $email = $_POST['email'];
    $password = $_POST['password'];
    
    $sql = "select * from customers WHERE email_address='$email' AND password = '$password'";
    $result = $conn->query($sql);
    if (mysqli_num_rows($result) == 1){
        
        if($row = $result->fetch_assoc()){
            //create session hereeeeeeee
            $_SESSION['id'] = $row['customer_id'];
            $_SESSION['email']=$row['email_address'];
            $_SESSION['fname']=$row['first_name'];
            $_SESSION['lname']=$row['last_name'];
            $_SESSION['balance']=$row['deposit_money'];
            $_SESSION['dob']=$row['dof'];
            $_SESSION['phone']=$row['phone_number'];
            
            
            header("location:Balance.php?loginsuccess");
            exit();
        }
        }else{
            header("location:index.php?loginfaild");
            exit();
        }
    }
    

}

 








