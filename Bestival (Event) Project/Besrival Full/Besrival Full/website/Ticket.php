<?php
include 'dbconnect.php';
include 'comments.inc.php';
session_start();
if(isset($_POST['indivi-buy-now'])){
    $fname=$_POST['fname'];
    $lname=$_POST['lname'];
    $email=$_POST['email'];
    $dob=$_POST['dob'];
    $phone=$_POST['phone'];
    $password=$_POST['password'];
    $camping_category=$_POST['camping_category'];
    $camping_number=$_POST['camping_number'];

    
    $f=substr($fname,0,2);
    $s= rand(10,100);
    $l=substr($lname,0,2);
    $m=rand(10,100);
    $n=substr($email,0,2);
    
    $reservationNumber= strtoupper($f.$s.$l.$m.$n);
    setcookie("fname", $fname, time() + 120);
    setcookie("lname", $lname, time() + 120);
    setcookie("email", $email, time() + 120);
    setcookie("dob", $dob, time() + 120);
    setcookie("phone", $phone, time() + 120);
    setcookie("password", $password, time() + 120);
    setcookie("reservation", $reservationNumber, time() + 120);
    setcookie("individual", "true", time() + 120);
    setcookie("camping_category", $camping_category, time() + 120);
    setcookie("camping_number", $camping_number, time() + 120);
    
    
    header("location:payment.php");
}

?>


<!-- Start header and nav    -->
<?php $pageName = 'Tickets'; include 'includes/Header-Nav.php' ?>
<!-- Start header and nav    -->
<section class="ticket ">
    <div class="container">
        <div class="row">
            <div class=" box col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class=" price-box text-center">
                    <h2>Bestival<span style="color:red"> (18+)</span></h2>
                    <p id="price" class=" center-block">$55</p>
                    <button id="click-first-form">Individual</button>
                    <button id="click-second-form">Group</button>
                    <button class="bt-camp">extra camping-spot</button>
                </div>
            </div>
        </div>
        <div class="row">
            <div class=" box col-lg-12 col-md-12 col-sm-12 col-xs-12  ">
                <div id="first-form">
                    <div class="individual-box">                 
                        <h2 class="text-center">Personal Information</h2> 
                        <form class="form-ticket" action="<?php echo $_SERVER['PHP_SELF']?>" method="post">
                            <div class="row1">
                                <label> First name *</label>
                                <input type="text" name='fname' />
                            </div>
                            <div class="row1">
                                <label> Last name * </label>
                                <input type="text" name='lname'/>
                            </div>
                            <div class="row1">
                                <label> Email address *	</label>
                                <input type="text" name='email'/>
                            </div>
                            <div class="row1">
                                <label> Date Of birth *	</label>
                                <input type="date" name="dob" placeholder="mm/dd/yyyy">
                            </div>
                            <div class="row1">
                                <label> Phone</label>
                                <input type="text" name='phone'/>
                            </div>  
                            <div class="row1">
                                <label> Password</label>
                                <input type="password" name='password'/>
                            </div>               
                            <input type="checkbox" name="camp1" value="camp" id="camping1" uncheacked> camping spot<br>
			    
                            <div class="select-camping1">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="selects" style="display:block; margin-bottom:20px">
                                            <select id="camp-l" name="camping_category">
                                                <option value="A">A</option>
                                                <option value="B">B</option>
                                                <option value="C">C</option>
                                                <option value="D">d</option>
                                                <option value="E">E</option>
                                                <option value="F">F</option>
                                                <option value="G">G</option>
                                                <option value="H">H</option>
                                                <option value="I">I</option>
                                                <option value="J">G</option>
                                                <option value="K">K</option>
                                                <option value="L">L</option>
                                                <option value="M">M</option>
                                                <option value="N">N</option>
                                                <option value="O">O</option>
                                                <option value="P">P</option>
                                                <option value="Q">Q</option>
                                                <option value="R">R</option>
                                                <option value="S">S</option>
                                                <option value="T">T</option>
                                                <option value="U">U</option>
                                                <option value="V">V</option>
                                                <option value="W">W</option>
                                                <option value="X">X</option>
                                                <option value="Y">Y</option>
                                            </select>  
                                                <select id="camp-nA" name='camping_number' >
						
                                                <?php
                                                    $sql = "select camping_number from camping where reserved = 0 AND camping_category = 'A' ORDER BY camping_number * 1 ASC";
                                                    $result = $conn->query($sql);
                                                    while($row = $result->fetch_assoc()){
                                                        
                                                        echo "<option value='".$row['camping_number']."'>".$row['camping_number']."</option>";
                                                    }
                                                ?>
                                            </select>
                                        </div>
                                        <p>hover on the map to zoom <?php echo $_POST['camp1']; ?></p>
                                        <span class='zoom' id='ex2' >
                                            <img id="map" src='img/map.png'  alt='camping-map'/>
                                        </span>
                                    </div>
                                </div>
                                <p>check full map <a href="full-map.php">Here</a></p>
                            </div>
                            <div class="row1 text-center">
                                <input type="submit" name="indivi-buy-now" value="Buy Now"/>
                                <input type="submit" value="Buy Later" id="buylater1"/>
                            </div>
                        </form>
                    </div>
                </div>
            </div>     
            <div class=" box col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div id="second-form">
                    <div class="group-box ">                 
                        <h2 class="text-center">Group Members Information</h2> 
                        <form class="form-ticket" action="payment.php" method="post">
                            <h3 >person 1 *</h3>
                            <div class="row1">
                                <label> First name *</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Last name * </label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Email address *	</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Date Of Birth *	</label>
                                <input type="date" name="bday" placeholder="mm/dd/yyyy">
                            </div>
                            <div class="row1">
                                <label> Phone</label>
                                <input type="text" />
                            </div> 
                            <div class="row1">
                                <label> Password</label>
                                <input type="password" />
                            </div>                           
                            <hr>
                            <h3>person 2 *</h3>
                            <div class="row1">
                                <label> First name *</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Last name * </label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Email address *	</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Date Of Birth *	</label>
                                <input type="date" name="bday" placeholder="mm/dd/yyyy">
                            </div>
                            <div class="row1">
                                <label> Phone</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Password</label>
                                <input type="password" />
                            </div> 
                            <hr>
                            <h3>person 3</h3>
                            <div class="row1">
                                <label> First name *</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Last name * </label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Email address *	</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Date Of Birth *	</label>
                                <input type="date" name="bday" placeholder="mm/dd/yyyy">
                            </div>
                            <div class="row1">
                                <label> Phone</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Password</label>
                                <input type="password" />
                            </div> 
                            <hr>
                            <h3>person 4</h3>
                            <div class="row1">
                                <label> First name *</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Last name * </label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Email address *	</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Date Of Birth *	</label>
                                <input type="date" name="bday" placeholder="mm/dd/yyyy">
                            </div>
                            <div class="row1">
                                <label> Phone</label>
                                <input type="text" />
                            </div>
                            <div class="row1">
                                <label> Password</label>
                                <input type="password" />
                            </div> 
                            <hr>
                            <input type="checkbox" name="camp2" value="camp" id="camping" uncheacked> camping spot<br>
                            <div class="select-camping">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="selects" style="display:block; margin-bottom:20px">
                                            <select id="camp-l">
                                                <option id="A">A</option>
                                                <option id="B">B</option>
                                                <option id="C">C</option>
                                                <option id="D">d</option>
                                                <option id="E">E</option>
                                                <option id="F">F</option>
                                                <option id="G">G</option>
                                                <option id="H">H</option>
                                                <option id="I">I</option>
                                                <option id="J">G</option>
                                                <option id="K">K</option>
                                                <option id="L">L</option>
                                                <option id="M">M</option>
                                                <option id="N">N</option>
                                                <option id="O">O</option>
                                                <option id="P">P</option>
                                                <option id="Q">Q</option>
                                                <option id="R">R</option>
                                                <option id="S">S</option>
                                                <option id="T">T</option>
                                                <option id="U">U</option>
                                                <option id="V">V</option>
                                                <option id="W">W</option>
                                                <option id="X">X</option>
                                                <option id="Y">Y</option>
                                            </select>  
                                                                                            <select id="camp-nA" >
                                                <?php
                                                    $sql = "select camping_number from camping where reserved = 0 ORDER BY camping_number * 1 ASC";
                                                    $result = $conn->query($sql);
                                                    while($row = $result->fetch_assoc()){
                                                        
                                                        echo "<option id='".$row['camping_number']."'>".$row['camping_number']."</option>";
                                                    }
                                                ?>
                                            </select>
                                        </div>
                                        <p>hover on the map to zoom</p>
                                        <span class='zoom' id='ex1' >
                                            <img id="map" src='img/map.png'  alt='camping-map'/>
                                        </span>
                                    </div>
                                </div>
                                <p>check full map <a href="full-map.php">Here</a></p>
                            </div>
                            <div class="row1 text-center">
                                <input type="submit" name="group-buy-now" value="Buy Now"/>
                                <input type="submit" value="Buy Later" id="buylater"/>
                            </div>
                        </form>
                    </div>
                </div> 
            </div>  
            <div class=" box col-lg-12 col-md-12 col-sm-12 col-xs-12  ">
                <div id="third-form">
                    <div class="camping-spot-box text-center">                 
                        <h2 class="text-center">Extra camping spot</h2> 
                        <form class="form-ticket" action="payment.php" method="post">
                            <div class="row1">
                                <input type="text" placeholder="Reservation Number"/>
                            </div>
                            <div class="select-camping">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="selects" style="display:block; margin-bottom:20px">
                                            <select id="camp-l">
                                                <option id="A">A</option>
                                                <option id="B">B</option>
                                                <option id="C">C</option>
                                                <option id="D">d</option>
                                                <option id="E">E</option>
                                                <option id="F">F</option>
                                                <option id="G">G</option>
                                                <option id="H">H</option>
                                                <option id="I">I</option>
                                                <option id="J">G</option>
                                                <option id="K">K</option>
                                                <option id="L">L</option>
                                                <option id="M">M</option>
                                                <option id="N">N</option>
                                                <option id="O">O</option>
                                                <option id="P">P</option>
                                                <option id="Q">Q</option>
                                                <option id="R">R</option>
                                                <option id="S">S</option>
                                                <option id="T">T</option>
                                                <option id="U">U</option>
                                                <option id="V">V</option>
                                                <option id="W">W</option>
                                                <option id="X">X</option>
                                                <option id="Y">Y</option>
                                            </select>  
                                                                                           <select id="camp-nA" >
                                                <?php
                                                    $sql = "select camping_number from camping where reserved = 0 ORDER BY camping_number * 1 ASC";
                                                    $result = $conn->query($sql);
                                                    while($row = $result->fetch_assoc()){
                                                        
                                                        echo "<option id='".$row['camping_number']."'>".$row['camping_number']."</option>";
                                                    }
                                                ?>
                                            </select>
                                        </div>
                                        <p>hover on the map to zoom</p>
                                        <span class='zoom' id='ex3' >
                                            <img id="map" src='img/map.png'  alt='camping-map'/>
                                        </span>
                                    </div>
                                </div>
                                <p>check full map <a href="full-map.php">Here</a></p>
                            </div>
                            <div class="row1 text-center">
                                <input id="camp_buy" type="submit" name="camp-buy-now" value="Buy"/>
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
