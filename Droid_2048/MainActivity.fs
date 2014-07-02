namespace Droid_2048

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

open GameGrid

[<Activity (Label = "Droid_2048", MainLauncher = true)>]
type MainActivity () =
    inherit Activity ()

    let mutable game = GameGrid.create()
    let mutable originX = 0.0
    let mutable originY = 0.0

    let play (dx :float) (dy :float) =
        let move = if (abs dx) > (abs dy) then 
                        (if (dx > 0.0) then GameGrid.Left else GameGrid.Right)
                    else
                        (if (dy > 0.0) then GameGrid.Up else GameGrid.Down)
       
        GameGrid.move game move

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        // Set our view from the "main" layout resource
        this.SetContentView (Resource_Layout.Main)

        // Get our button from the layout resource, and attach an event to it
        let button = this.FindViewById<Button>(Resource_Id.reset)
        let score = this.FindViewById<TextView>(Resource_Id.score)
        let board = this.FindViewById<TextView>(Resource_Id.board)
        let refreshGame () = 
            score.Text <- sprintf "%d" (game.score)
            board.Text <- GameGrid.toString game
  
        button.Click.Add (fun args -> 
            game <- GameGrid.create()
            refreshGame ()
        )

        board.Touch.Add (fun (args :View.TouchEventArgs) ->
             match args.Event.Action with
                | MotionEventActions.Down ->
                    originX <- (float) args.Event.RawX
                    originY <- (float) args.Event.RawY
                | MotionEventActions.Up ->
                    let deltaX = originX - (float) args.Event.RawX
                    let deltaY = originY - (float) args.Event.RawY
                    originX <- 0.0
                    originY <- 0.0
                    game <- (play deltaX deltaY)
                    refreshGame ()
                | _ ->
                    () // skipe
        )