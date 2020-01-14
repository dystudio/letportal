import { Injectable } from '@angular/core';
import { MatSnackBar, MatDialog, MatSnackBarRef } from '@angular/material';
import { ActionNotificationComponent } from './action-natification/action-notification.component';
import { MessageType, ToastType } from './shortcut.models';
import { ConfirmationDialogComponent } from './custom-action-dialog/custom-action-dialog.component';
import { ToastrService } from 'ngx-toastr';


@Injectable({
	providedIn: 'root'
})
export class ShortcutUtil {
	constructor(private snackBar: MatSnackBar,
		private dialog: MatDialog,
		private toastr: ToastrService) { }

	// SnackBar for notifications
	showActionNotification(
		message: string,
		type: MessageType = MessageType.Create,
		duration: number = 10000,
		showCloseButton: boolean = true,
		showUndoButton: boolean = false,
		undoButtonDuration: number = 3000,
		verticalPosition: 'top' | 'bottom' = 'top'
	): MatSnackBarRef<any> {
		return this.snackBar.openFromComponent(ActionNotificationComponent, {
			duration: duration,
			data: {
				message,
				snackBar: this.snackBar,
				showCloseButton: showCloseButton,
				showUndoButton: showUndoButton,
				undoButtonDuration,
				verticalPosition,
				type,
				action: 'Undo'
			},
			verticalPosition: verticalPosition
		});
	}

	// Method returns instance of MatDialog
	confirmationDialog(
		title: string = '', 
		description: string = '', 
		waitDesciption: string = '', 
		messType: MessageType = MessageType.Create,
		confirmText: string = '') {
		return this.dialog.open(ConfirmationDialogComponent, {
			data: { title, description, waitDesciption, messType, confirmText },
			width: '440px',
		});
	}

	toastMessage(
		message: string,
		type: ToastType = ToastType.Info,
		title?: string,
		allowHtmlMessage: boolean = false,
		timeout: number = 3000
	) {
		// Important note: that is a trick for avoid 'outside' angular lifecycle changedetectionstrategy
		// Issue: https://github.com/angular/angular/issues/15634
		setTimeout(() => {
			switch (type) {
				case ToastType.Info:
					this.toastr.info(message, title, {
						timeOut: timeout,
						enableHtml: allowHtmlMessage
					})
					break
				case ToastType.Success:
					this.toastr.success(message, title, {
						timeOut: timeout,
						enableHtml: allowHtmlMessage
					})
					break
				case ToastType.Warning:
					this.toastr.warning(message, title, {
						timeOut: timeout,
						enableHtml: allowHtmlMessage
					})
					break
				case ToastType.Error:
					this.toastr.error(message, title, {
						timeOut: timeout,
						enableHtml: allowHtmlMessage
					})
					break
			}
		},300)		
	}
}
